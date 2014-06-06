using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class CommentLikesService : ICommentLikes
    {
        public List<CommentLike> Get(int commentId)
        {
            return CommentLikesFactory.GetInstance().CreateCommentLikes().Get(commentId);
        }

        public CommentLike Add(CommentLike commentLike)
        {
            return CommentLikesFactory.GetInstance().CreateCommentLikes().Add(commentLike);
        }
    }
}
