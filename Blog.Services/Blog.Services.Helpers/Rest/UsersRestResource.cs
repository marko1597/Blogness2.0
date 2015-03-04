using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class UsersRestResource : IUsersResource
    {
        public List<User> GetUsers(int threshold = 10, int skip = 10)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetUsers(threshold, skip);
            }
        }
        public List<User> GetUsersByCommunity(int communityId, int threshold = 5, int skip = 10)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetUsersByCommunity(communityId, threshold, skip);
            }
        }


        public List<User> GetUsersWithNoIdentityId()
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetUsersWithNoIdentityId();
            }
        }

        public User GetByUserName(string username)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetByUserName(username);
            }
        }

        public User GetByIdentityId(string identityId)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetByIdentityId(identityId);
            }
        }

        public User Get(int userId)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.Get(userId);
            }
        }

        public User Add(User user)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.Add(user);
            }
        }

        public User Update(User user)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.Update(user);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
