using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class CommentLikesService : ICommentLikes
    {
        public List<CommentLike> Get(int commentId)
        {
            return CommentLikesFactory.GetInstance().CreateCommentLikes().Get(commentId);
        }

        public void Add(CommentLike commentLike)
        {
            CommentLikesFactory.GetInstance().CreateCommentLikes().Add(commentLike);
        }
    }
}
