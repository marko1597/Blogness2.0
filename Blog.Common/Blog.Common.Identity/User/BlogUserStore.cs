using System.Collections.Generic;
using System.Linq;
using Blog.Common.Identity.Repository;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity.User
{
    public class BlogUserStore : UserStore<BlogUser>
    {
        public BlogUserStore(IdentityDbContext<BlogUser> context) : base(context)
        {
        }

        public List<BlogUser> GetBlogUsers()
        {
            List<BlogUser> users;
            using (var context = new BlogIdentityDbContext())
            {
                users = context.Users.ToList();
            }
            return users;
        }
    }
}
