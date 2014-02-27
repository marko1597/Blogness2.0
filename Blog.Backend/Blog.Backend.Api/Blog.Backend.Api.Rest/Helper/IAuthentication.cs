using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Api.Rest.Helper
{
    public interface IAuthentication
    {
        bool SignIn(User user);
    }
}
