using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation
{
    public interface ICommentLikes
    {
        List<CommentLike> Get(int commentId);
        void Add(CommentLike commentLike);
    }
}
