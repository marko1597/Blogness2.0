using Blog.Common.Identity.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Blog.Common.Identity.User
{
    public class BlogUserManager : UserManager<BlogUser>
    {
        public BlogUserManager(IUserStore<BlogUser> store)
            : base(store)
        {
        }

        public static BlogUserManager Create(IdentityFactoryOptions<BlogUserManager> options, IOwinContext context)
        {
            var manager = new BlogUserManager(new BlogUserStore(context.Get<BlogIdentityDbContext>()));
            manager.UserValidator = new UserValidator<BlogUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<BlogUser>(
                    dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
