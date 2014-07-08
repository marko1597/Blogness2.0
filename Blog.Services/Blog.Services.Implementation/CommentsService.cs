using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class CommentsService : BaseService, ICommentsService
    {
        private readonly ICommentsLogic _commentsLogic;

        public CommentsService(ICommentsLogic commentsLogic)
        {
            _commentsLogic = commentsLogic;
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
            return _commentsLogic.Add(comment);
        }

        public bool Delete(int id)
        {
            return _commentsLogic.Delete(id);
        }
    }
}
