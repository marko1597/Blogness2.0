using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IUsersResource : IUsersService
    {
    }

    public interface IUsersRestResource
    {
        List<User> GetUsers(int threshold = 10, int skip = 10);
        List<User> GetUsersByCommunity(int communityId, int threshold = 5, int skip = 10);
        List<User> GetUsersWithNoIdentityId();
        User GetByUserName(string username);
        User GetByIdentityId(string identityId);
        User Get(int userId);
        User Add(User user, string authenticationToken);
        User Update(User user, string authenticationToken);
    }
}
