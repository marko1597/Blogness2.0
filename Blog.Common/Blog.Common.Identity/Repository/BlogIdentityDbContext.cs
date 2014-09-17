using System.Data.Entity;
using Blog.Common.Identity.User;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity.Repository
{
    public class BlogIdentityDbContext : IdentityDbContext<BlogUser>
    {
        public BlogIdentityDbContext() : base("BlogIdentityDb", false)
        {
            Database.SetInitializer(new BlogIdentityDbInitializer());
        }

        public static BlogIdentityDbContext Create()
        {
            return new BlogIdentityDbContext();
        }

        public class BlogIdentityDbInitializer : DropCreateDatabaseIfModelChanges<BlogIdentityDbContext>
        {
        }
    }
}
