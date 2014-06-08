using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class CommentsService : IComments
    {
        public List<Comment> GetByPostId(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().GetByPostId(id);
        }

        public List<Comment> GetByUser(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().GetByUser(id);
        }

        public List<Comment> GetReplies(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().GetReplies(id);
        }

        public Comment Add(Comment comment)
        {
            return CommentsFactory.GetInstance().CreateComments().Add(comment);
        }

        public bool Delete(int id)
        {
            return CommentsFactory.GetInstance().CreateComments().Delete(id);
        }
    }
}
