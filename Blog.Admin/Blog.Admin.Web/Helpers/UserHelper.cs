using System.Threading.Tasks;
using System.Web;
using Blog.Common.Contracts;
using Blog.Common.Identity.Models;
using Blog.Common.Identity.User;
using Blog.Services.Helpers.Wcf;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity.Owin;

namespace Blog.Admin.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private BlogUserManager _userManager;
        public BlogUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().Get<BlogUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private IUsersResource _usersResource;
        public IUsersResource UsersResource
        {
            get { return _usersResource ?? new UsersResource(); }
            set { _usersResource = value; }
        }

        public async Task<User> AddBlogUser(BlogRegisterModel model)
        {
            var identityUser = await UserManager.FindByNameAsync(model.Username);
            var blogUser = UsersResource.Add(new User
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                IdentityId = identityUser.Id,
                EmailAddress = model.Email
            });

            return blogUser;
        }

        public User DeleteUser(string username)
        {
            var user = UsersResource.GetByUserName(username);
            user.IsDeleted = true;

            var result = UsersResource.Update(user);
            return result;
        }
    }
}