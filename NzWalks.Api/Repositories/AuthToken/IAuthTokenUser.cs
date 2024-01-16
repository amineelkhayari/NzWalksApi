using Microsoft.AspNetCore.Identity;

namespace NzWalks.Api.Repositories.AuthToken
{
    public interface IAuthTokenUser
    {
        public string CreateJwtToken(IdentityUser user, List<string> Roles);
    }
}
