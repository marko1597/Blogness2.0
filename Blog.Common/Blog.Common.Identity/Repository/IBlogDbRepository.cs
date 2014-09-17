using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Common.Identity.Models;
using Blog.Common.Identity.User;
using Microsoft.AspNet.Identity;

namespace Blog.Common.Identity.Repository
{
    public interface IBlogDbRepository
    {
        Task<IdentityResult> RegisterUser(BlogRegisterModel userModel);
        Task<BlogUser> FindUser(string userName, string password);
        Task<IdentityResult> AddClaim(string userId, Claim claim);
        Task<IdentityResult> RemoveClaim(string userId, Claim claim);
        Task<IList<Claim>> GetClaims(string userId);
    }
}
