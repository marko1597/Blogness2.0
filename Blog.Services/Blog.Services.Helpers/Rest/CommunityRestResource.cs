using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class CommunityRestResource : ICommunityResource
    {
        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }

        public Community Get(int communityId)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.Get(communityId);
            }
        }

        public List<Community> GetList()
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetList();
            }
        }

        public List<Community> GetMore(int skip)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetMore(skip);
            }
        }

        public List<Community> GetJoinedByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetJoinedByUser(userId);
            }
        }

        public List<Community> GetMoreJoinedByUser(int userId, int skip)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetMoreJoinedByUser(userId, skip);
            }
        }

        public List<Community> GetCreatedByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetCreatedByUser(userId);
            }
        }

        public List<Community> GetMoreCreatedByUser(int userId, int skip)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.GetMoreCreatedByUser(userId, skip);
            }
        }

        public Community Add(Community community)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.Add(community);
            }
        }

        public Community Update(Community community)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.Update(community);
            }
        }

        public bool Delete(int communityId)
        {
            using (var svc = new ServiceProxyHelper<ICommunityService>("CommunityService"))
            {
                return svc.Proxy.Delete(communityId);
            }
        }
    }
}
