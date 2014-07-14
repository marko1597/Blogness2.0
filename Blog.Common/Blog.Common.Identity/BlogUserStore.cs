using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity
{
    public class BlogUserStore : UserStore<BlogUser>
    {
        public BlogUserStore(IdentityDbContext<BlogUser> context) : base(context)
        {
        }
    }
}
