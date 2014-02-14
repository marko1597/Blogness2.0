using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
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

        public Comment Add(Comment comment)
        {
            return CommentsFactory.GetInstance().CreateCommentLikes().Add(comment);
        }

        public void Delete(int id)
        {
            CommentsFactory.GetInstance().CreateCommentLikes().Delete(id);
        }
    }
}
