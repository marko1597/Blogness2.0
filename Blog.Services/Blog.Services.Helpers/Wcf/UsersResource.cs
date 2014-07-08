using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class UsersResource : BaseResource, IUsersResource
    {
        public User GetByCredentials(string username, string password)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetByCredentials(username, password);
            }
        }

        public User GetByUserName(string username)
        {
            using (var svc = new ServiceProxyHelper<IUsersService>("UsersService"))
            {
                return svc.Proxy.GetByUserName(username);
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
    }
}
