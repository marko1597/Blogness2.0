using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class CommentsRestResource : ICommentsResource
    {
        public Comment Get(int commentId)
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.Get(commentId);
            }
        }

        public List<Comment> GetByPostId(int id)
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.GetByPostId(id);
            }
        }

        public List<Comment> GetByUser(int id)
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.GetByUser(id);
            }
        }

        public List<Comment> GetReplies(int id)
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.GetReplies(id);
            }
        }

        public Comment Add(Comment comment)
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.Add(comment);
            }
        }

        public bool Delete(int id)
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.Delete(id);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<ICommentsService>("CommentsService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
