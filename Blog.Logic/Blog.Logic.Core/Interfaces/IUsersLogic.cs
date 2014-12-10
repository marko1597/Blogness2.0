using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IUsersLogic
    {
        List<User> GetUsers(int threshold = 10, int skip = 10);
        List<User> GetUsersByCommunity(int communityId, int threshold = 5, int skip = 10);
        List<User> GetUsersWithNoIdentityId();
        User GetByUserName(string userName);
        User GetByIdentity(string identityId);
        User Get(int userId);
        User Add(User user);
        User Update(User user);
        bool IsValidEmailAddress(string emailaddress);
    }
}
