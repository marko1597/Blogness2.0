using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class CommentLikesService : BaseService, ICommentLikesService
    {
        private readonly ICommentLikesLogic _commentLikesLogic;

        public CommentLikesService(ICommentLikesLogic commentLikesLogic)
        {
            _commentLikesLogic = commentLikesLogic;
        }

        public List<CommentLike> Get(int commentId)
        {
            return _commentLikesLogic.Get(commentId);
        }

        public CommentLike Add(CommentLike commentLike)
        {
            return _commentLikesLogic.Add(commentLike);
        }
    }
}
