using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class ViewCountRestResource : IViewCountResource
    {
        public List<ViewCount> Get(int postId)
        {
            using (var svc = new ServiceProxyHelper<IViewCountService>("ViewCountService"))
            {
                return svc.Proxy.Get(postId);
            }
        }

        public void Add(ViewCount viewCount)
        {
            using (var svc = new ServiceProxyHelper<IViewCountService>("ViewCountService"))
            {
                svc.Proxy.Add(viewCount);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IViewCountService>("ViewCountService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
