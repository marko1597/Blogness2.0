using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
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
