using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
{
    public class CommentsService : IComments
    {
        public List<Comment> GetByPostId(int id)
        {
            return CommentsFactory.GetInstance().CreateCommentLikes().GetByPostId(id);
        }

        public List<Comment> GetByUser(int id)
        {
            return CommentsFactory.GetInstance().CreateCommentLikes().GetByUser(id);
        }

        public List<Comment> GetReplies(int id)
        {
            return CommentsFactory.GetInstance().CreateCommentLikes().GetReplies(id);
        }

        public bool Add(Comment comment)
        {
            return CommentsFactory.GetInstance().CreateCommentLikes().Add(comment);
        }

        public bool Delete(int id)
        {
            return CommentsFactory.GetInstance().CreateCommentLikes().Delete(id);
        }
    }
}
