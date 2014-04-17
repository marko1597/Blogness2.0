using Blog.Backend.Common.Contracts;

namespace Blog.Frontend.Common.Authentication
{
    public interface IAuthentication
    {
        bool SignIn(User user);
    }
}
