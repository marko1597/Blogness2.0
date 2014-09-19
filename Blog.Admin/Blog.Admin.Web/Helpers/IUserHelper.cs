using System.Threading.Tasks;
using Blog.Common.Contracts;
using Blog.Common.Identity.Models;

namespace Blog.Admin.Web.Helpers
{
    public interface IUserHelper
    {
        Task<User> AddBlogUser(BlogRegisterModel model);
        User DeleteUser(string username);
    }
}
