using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class CommentLikeResource : ICommentLikeResource
    {
        public List<CommentLike> Get(Func<CommentLike, bool> expression)
        {
            return new DbGet().CommentLikes(expression);
        }

        public CommentLike Add(CommentLike commentLike)
        {
            return new DbAdd().CommentLike(commentLike);
        }

        public bool Delete(CommentLike commentLike)
        {
            return new DbDelete().CommentLike(commentLike);
        }
    }
}
