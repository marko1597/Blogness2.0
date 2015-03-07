using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class CommentLikesRestResource : ICommentLikesRestResource
    {
        public List<CommentLike> Get(int commentId)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<List<CommentLike>>(
                    svc.Get(Constants.BlogRestUrl, string.Format("comments/{0}/likes", commentId)));
                return result;
            }
        }

        public void Add(int commentId, string username, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var commentLikeDummy = new CommentLike();
                svc.Post(Constants.BlogRestUrl, 
                    string.Format("comments/likes?commentId={0}&username={1}", commentId, username),
                    commentLikeDummy, authenticationToken);
            }
        }
    }
}
