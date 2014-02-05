using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface ICommentLikes
    {
        List<CommentLike> Get(int commentId);
        void Add(CommentLike commentLike);
    }
}
