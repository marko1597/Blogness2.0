using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
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
