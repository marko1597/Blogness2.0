using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Common.Web.Authentication
{
    public interface IAuthenticationHelper
    {
        bool SignIn(User user);
    }
}
