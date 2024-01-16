using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NzWalks.Api.Repositories.AuthToken
{
    public class AuthTokenUser : IAuthTokenUser
    {
        private readonly IConfiguration configuration;
 
        public AuthTokenUser(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        string IAuthTokenUser.CreateJwtToken(IdentityUser user, List<string> Roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", user.Email));
            foreach (var item in Roles)
            {
                claims.Add(new Claim("role", item));

            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
