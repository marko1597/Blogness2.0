using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface ICommentLikesResource : IBaseResource
    {
        List<CommentLike> Get(int commentId);
        CommentLike Add(CommentLike commentLike);
    }
}
