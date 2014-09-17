using Blog.Common.Identity.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Blog.Common.Identity.Role
{
    public class BlogRoleManager
    {
        public class ApplicationRoleManager : RoleManager<BlogRole>
        {
            public ApplicationRoleManager(IRoleStore<BlogRole, string> roleStore)
                : base(roleStore)
            {
            }

            public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
            {
                return new ApplicationRoleManager(new RoleStore<BlogRole>(context.Get<BlogIdentityDbContext>()));
            }
        }
    }
}
