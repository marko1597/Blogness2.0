using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IComments
    {
        List<Comment> GetByPostId(int postId);
        List<Comment> GetReplies(int commentId);
        Comment Add(Comment comment);
        void Delete(int commentId);
    }
}
