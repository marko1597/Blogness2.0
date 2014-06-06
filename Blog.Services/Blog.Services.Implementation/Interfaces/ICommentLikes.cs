using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface ICommentLikes
    {
        List<CommentLike> Get(int commentId);
        CommentLike Add(CommentLike commentLike);
    }
}
