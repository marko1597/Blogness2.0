using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation
{
    public interface IComments
    {
        List<Comment> GetByPostId(int postId);
        List<Comment> GetByUser(int userId);
        List<Comment> GetReplies(int commentId);
        bool Add(Comment comment);
        bool Delete(int commentId);
    }
}
