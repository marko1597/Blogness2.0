using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Common.Identity.Role
{
    public class BlogRole : IdentityRole
    {
        public BlogRole()
        { 
        }

        public BlogRole(string name) : base(name) { }

        public string Description { get; set; }

    }
}
