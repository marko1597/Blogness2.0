using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class SessionResource : BaseResource, ISessionResource
    {
        public List<Session> GetAll()
        {
            using (var svc = new ServiceProxyHelper<ISessionService>("SessionService"))
            {
                return svc.Proxy.GetAll();
            }
        }

        public Session GetByUser(string username)
        {
            using (var svc = new ServiceProxyHelper<ISessionService>("SessionService"))
            {
                return svc.Proxy.GetByUser(username);
            }
        }

        public Session GetByIp(string ipAddress)
        {
            using (var svc = new ServiceProxyHelper<ISessionService>("SessionService"))
            {
                return svc.Proxy.GetByIp(ipAddress);
            }
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            using (var svc = new ServiceProxyHelper<ISessionService>("SessionService"))
            {
                return svc.Proxy.Login(userName, passWord, ipAddress);
            }
        }

        public Error Logout(string userName)
        {
            using (var svc = new ServiceProxyHelper<ISessionService>("SessionService"))
            {
                return svc.Proxy.Logout(userName);
            }
        }
    }
}
