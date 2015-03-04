using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface ICommentsResource : ICommentsService
    {
    }

    public interface ICommentsRestResource 
    {
        Comment Get(int commentId);
        List<Comment> GetByPostId(int postId);
        List<Comment> GetByUser(int userId);
        List<Comment> GetReplies(int commentId);
        Comment Add(Comment comment, string authenticationToken);
        bool Delete(int commentId, string authenticationToken);
    }
}
