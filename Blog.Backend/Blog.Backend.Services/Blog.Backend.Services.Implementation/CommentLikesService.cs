using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
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
