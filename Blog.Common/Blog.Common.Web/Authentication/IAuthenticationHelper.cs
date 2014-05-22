using Blog.Common.Contracts;

namespace Blog.Common.Web.Authentication
{
    public interface IAuthenticationHelper
    {
        bool SignIn(User user);
        bool SignOut(User user);
    }
}
