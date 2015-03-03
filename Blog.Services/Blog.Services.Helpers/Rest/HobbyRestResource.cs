using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class HobbyRestResource : IHobbyResource
    {
        public List<Hobby> GetByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<IHobbyService>("HobbyService"))
            {
                return svc.Proxy.GetByUser(userId);
            }
        }

        public Hobby Add(Hobby hobby)
        {
            using (var svc = new ServiceProxyHelper<IHobbyService>("HobbyService"))
            {
                return svc.Proxy.Add(hobby);
            }
        }

        public Hobby Update(Hobby hobby)
        {
            using (var svc = new ServiceProxyHelper<IHobbyService>("HobbyService"))
            {
                return svc.Proxy.Update(hobby);
            }
        }

        public bool Delete(int hobbyId)
        {
            using (var svc = new ServiceProxyHelper<IHobbyService>("HobbyService"))
            {
                return svc.Proxy.Delete(hobbyId);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IHobbyService>("HobbyService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
