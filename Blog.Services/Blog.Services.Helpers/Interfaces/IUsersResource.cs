using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IUsersResource : IUsersService
    {
    }

    public interface IUsersRestResource
    {
        User GetByUserName(string username);
        User Get(int userId);
        User Add(User user, string authenticationToken);
        User Update(User user, string authenticationToken);
    }
}
