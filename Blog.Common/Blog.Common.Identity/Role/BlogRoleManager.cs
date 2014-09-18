using Blog.Common.Identity.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Blog.Common.Identity.Role
{
    public class BlogRoleManager : RoleManager<BlogRole>
    {
        public BlogRoleManager(IRoleStore<BlogRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static BlogRoleManager Create(IdentityFactoryOptions<BlogRoleManager> options, IOwinContext context)
        {
            return new BlogRoleManager(new RoleStore<BlogRole>(context.Get<BlogIdentityDbContext>()));
        }
    }
}
