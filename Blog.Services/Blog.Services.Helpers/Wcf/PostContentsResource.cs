using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class PostContentsResource : BaseResource, IPostContentsResource
    {
        public List<PostContent> GetByPostId(int postId)
        {
            using (var svc = new ServiceProxyHelper<IPostContentsService>("PostContentsService"))
            {
                return svc.Proxy.GetByPostId(postId);
            }
        }

        public PostContent Get(int postContentId)
        {
            using (var svc = new ServiceProxyHelper<IPostContentsService>("PostContentsService"))
            {
                return svc.Proxy.Get(postContentId);
            }
        }

        public PostContent Add(PostContent postImage)
        {
            using (var svc = new ServiceProxyHelper<IPostContentsService>("PostContentsService"))
            {
                return svc.Proxy.Add(postImage);
            }
        }

        public bool Delete(int postContentId)
        {
            using (var svc = new ServiceProxyHelper<IPostContentsService>("PostContentsService"))
            {
                return svc.Proxy.Delete(postContentId);
            }
        }
    }
}
