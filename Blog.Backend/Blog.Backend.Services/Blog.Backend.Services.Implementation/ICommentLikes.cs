using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface ICommentLikes
    {
        List<CommentLike> Get(int commentId);
        void Add(CommentLike commentLike);
    }
}
