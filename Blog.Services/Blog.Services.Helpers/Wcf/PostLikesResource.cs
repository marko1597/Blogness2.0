using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class PostLikesResource : IPostLikesResource
    {
        public List<PostLike> Get(int postId)
        {
            using (var svc = new ServiceProxyHelper<IPostLikesService>("PostLikesService"))
            {
                return svc.Proxy.Get(postId);
            }
        }

        public void Add(PostLike postLike)
        {
            using (var svc = new ServiceProxyHelper<IPostLikesService>("PostLikesService"))
            {
                svc.Proxy.Add(postLike);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IPostLikesService>("PostLikesService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
