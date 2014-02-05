using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface ICommentResource
    {
        List<Comment> Get(Func<Comment, bool> expression);
        List<Comment> Get(Func<Comment, bool> expression, bool isComplete);
        Comment Add(Comment comment);
        Comment Update(Comment comment);
        bool Delete(Comment comment);
    }
}
