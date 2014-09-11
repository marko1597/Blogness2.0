using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class CommentsService : BaseService, ICommentsService
    {
        private readonly ICommentsLogic _commentsLogic;
        private readonly IRedisService _redisService;

        public CommentsService(ICommentsLogic commentsLogic, IRedisService redisService)
        {
            _commentsLogic = commentsLogic;
            _redisService = redisService;
        }

        public Comment Get(int commentId)
        {
            return _commentsLogic.Get(commentId);
        }

        public List<Comment> GetByPostId(int id)
        {
            return _commentsLogic.GetByPostId(id);
        }

        public List<Comment> GetByUser(int id)
        {
            return _commentsLogic.GetByUser(id);
        }

        public List<Comment> GetReplies(int id)
        {
            return _commentsLogic.GetReplies(id);
        }

        public Comment Add(Comment comment)
        {
            var result = _commentsLogic.Add(comment);
            if (result.Error == null) return result;

            var commentAdded = new CommentAdded
            {
                CommentId = comment.Id,
                PostId = comment.PostId,
                Comment = result,
                ClientFunction = Constants.SocketClientFunctions.commentAdded.ToString()
            };

            _redisService.Publish(commentAdded);
            return result;
        }

        public bool Delete(int id)
        {
            return _commentsLogic.Delete(id);
        }
    }
}
