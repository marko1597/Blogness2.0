using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class CommentLikesResource : ICommentLikesResource
    {
        public List<CommentLike> Get(int commentId)
        {
            using (var svc = new ServiceProxyHelper<ICommentLikesService>("CommentLikesService"))
            {
                return svc.Proxy.Get(commentId);
            }
        }

        public void Add(CommentLike commentLike)
        {
            using (var svc = new ServiceProxyHelper<ICommentLikesService>("CommentLikesService"))
            {
                svc.Proxy.Add(commentLike);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<ICommentLikesService>("CommentLikesService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
