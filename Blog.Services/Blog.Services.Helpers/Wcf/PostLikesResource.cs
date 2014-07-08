using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class PostLikesResource : BaseResource, IPostLikesResource
    {
        public List<PostLike> Get(int postId)
        {
            using (var svc = new ServiceProxyHelper<IPostLikesService>("PostLikesService"))
            {
                return svc.Proxy.Get(postId);
            }
        }

        public PostLike Add(PostLike postLike)
        {
            using (var svc = new ServiceProxyHelper<IPostLikesService>("PostLikesService"))
            {
                return svc.Proxy.Add(postLike);
            }
        }
    }
}
