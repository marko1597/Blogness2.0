using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface ICommentsLogic
    {
        Comment Get(int commentId);
        List<Comment> GetByPostId(int postId);
        List<Comment> GetByUser(int userId);
        List<Comment> GetTopComments(int postId, int commentsCount);
        List<Comment> GetReplies(int commentId);
        Comment Add(Comment comment);
        bool Delete(int commentId);
    }
}
