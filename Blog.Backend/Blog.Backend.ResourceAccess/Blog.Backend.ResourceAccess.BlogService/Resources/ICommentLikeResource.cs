using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface ICommentLikeResource
    {
        List<CommentLike> Get(Func<CommentLike, bool> expression);
        CommentLike Add(CommentLike commentLike);
        bool Delete(CommentLike commentLike);
    }
}
