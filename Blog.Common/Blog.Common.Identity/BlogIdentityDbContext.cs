using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity
{
    public class BlogIdentityDbContext : IdentityDbContext<BlogUser>
    {
        public BlogIdentityDbContext() : base("BlogIdentityDb", false)
        {
        }

        public static BlogIdentityDbContext Create()
        {
            return new BlogIdentityDbContext();
        }
    }
}
