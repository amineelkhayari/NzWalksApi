using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Api.Models.Domain;
using NzWalks.Api.Models.DTO;
using NzWalks.Api.Models.DTO.AuthDtos;
using NzWalks.Api.Models.DTO.ImageDtos;
using NzWalks.Api.Repositories.AuthToken;

namespace NzWalks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAuthTokenUser authTokenUser;


        public AuthController(UserManager<IdentityUser> userManager, IAuthTokenUser authTokenUser)
        {
            this.userManager = userManager;
            this.authTokenUser = authTokenUser;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAtuth([FromBody] AddAuthDto addAuthDto )
        {
            var IdentityUser = new IdentityUser()
            {
                UserName=addAuthDto.UserName,
                Email=addAuthDto.UserName
            };
            var IdentityResult = await userManager.CreateAsync(IdentityUser, addAuthDto.Password); 
            if(IdentityResult.Succeeded)
            {
                if(addAuthDto.Roles != null && addAuthDto.Roles.Any())
                {
                    IdentityResult = await userManager.AddToRolesAsync(IdentityUser, addAuthDto.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("user was registered pls login");
                    }
                }
                return Ok("");
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("test")]
        public async Task<ImageUploadRequestDto> test([FromForm] ImageUploadRequestDto name)
        {

            return name;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginAuthDto lauthdto)
        {
            var user = await userManager.FindByEmailAsync(lauthdto.UserName);
            if(user != null)
            {
                var checkpassResult =await userManager.CheckPasswordAsync(user, lauthdto.Password);
                if (checkpassResult)
                {
                    var role = await userManager.GetRolesAsync(user);
                    //create A token
                    if(role != null)
                    {
                    var jwtToken = authTokenUser.CreateJwtToken(user, role.ToList());
                        var jwtResponse = new LoginResponseDto()
                        {
                            JwtToken = jwtToken
                        };
                     return Ok(jwtResponse);
                    }
                   

                }
            }
            return BadRequest("UserName Or Password Incorrect");
        }
    }
}
