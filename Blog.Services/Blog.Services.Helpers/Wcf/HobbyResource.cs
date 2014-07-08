using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class HobbyResource : BaseResource, IHobbyResource
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
    }
}
