using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Common.Identity.Interfaces;
using Microsoft.AspNet.Identity;

namespace Blog.Common.Identity
{
    public class BlogDbRepository : IDisposable, IBlogDbRepository
    {
        private readonly BlogIdentityDbContext _ctx;
        private readonly BlogUserManager _userManager;

        public BlogDbRepository()
        {
            _ctx = new BlogIdentityDbContext();
            _userManager = new BlogUserManager(new BlogUserStore(_ctx));
        }
 
        public async Task<IdentityResult> RegisterUser(BlogRegisterModel userModel)
        {
            var user = new BlogUser
            {
                UserName = userModel.Username
            };
 
            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<BlogUser> FindUser(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<IdentityResult> AddClaim(string userId, Claim claim)
        {
            var result = await _userManager.AddClaimAsync(userId, claim);
            return result;
        }

        public async Task<IdentityResult> RemoveClaim(string userId, Claim claim)
        {
            var result = await _userManager.RemoveClaimAsync(userId, claim);
            return result;
        }

        public async Task<IList<Claim>> GetClaims(string userId)
        {
            var result = await _userManager.GetClaimsAsync(userId);
            return result;
        }
 
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
 
        }
    }
}
