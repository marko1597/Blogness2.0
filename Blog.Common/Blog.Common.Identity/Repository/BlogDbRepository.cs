using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Common.Identity.Models;
using Blog.Common.Identity.Role;
using Blog.Common.Identity.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity.Repository
{
    public class BlogDbRepository : IDisposable, IBlogDbRepository
    {
        private readonly BlogIdentityDbContext _ctx;
        private readonly BlogUserManager _userManager;
        private readonly BlogRoleManager _roleManager;

        public BlogDbRepository()
        {
            _ctx = new BlogIdentityDbContext();
            _userManager = new BlogUserManager(new BlogUserStore(_ctx));
            _roleManager = new BlogRoleManager(new RoleStore<BlogRole>(_ctx));
        }
 
        public async Task<IdentityResult> RegisterUser(BlogRegisterModel userModel)
        {
            var user = new BlogUser
            {
                UserName = userModel.Username,
                Email = userModel.Email
            };
 
            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<BlogUser> FindUsers(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user;
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

        public async Task<IdentityResult> CreateRoleAsync(BlogRole blogRole)
        {
            var roleresult = await _roleManager.CreateAsync(blogRole);
            return roleresult;
        }

        public async Task<IdentityResult> AddToRolesAsync(string id, string[] roles)
        {
            var result = await _userManager.AddToRolesAsync(id, roles);
            return result;
        }

        public IEnumerable<BlogRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
 
        }
    }
}
