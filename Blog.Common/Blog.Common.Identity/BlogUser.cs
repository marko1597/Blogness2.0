using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity
{
    public class BlogUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BlogUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Name, userIdentity.GetUserName())
                });

            return userIdentity;
        }
    }
}
