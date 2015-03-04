using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class UsersRestResource : IUsersRestResource
    {
        public List<User> GetUsers(int threshold = 10, int skip = 10)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetUsersByCommunity(int communityId, int threshold = 5, int skip = 10)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetUsersWithNoIdentityId()
        {
            throw new System.NotImplementedException();
        }

        public User GetByUserName(string username)
        {
            throw new System.NotImplementedException();
        }

        public User GetByIdentityId(string identityId)
        {
            throw new System.NotImplementedException();
        }

        public User Get(int userId)
        {
            throw new System.NotImplementedException();
        }

        public User Add(User user, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public User Update(User user, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
