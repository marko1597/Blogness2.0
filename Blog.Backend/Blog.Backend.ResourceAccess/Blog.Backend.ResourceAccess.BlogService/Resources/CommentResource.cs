using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class CommentResource : ICommentResource
    {
        public List<Comment> Get(Func<Comment, bool> expression)
        {
            return new DbGet().Comments(expression);
        }

        public List<Comment> Get(Func<Comment, bool> expression, bool isComplete)
        {
            return new DbGet().Comments(expression, isComplete);
        }

        public Comment Add(Comment comment)
        {
            return new DbAdd().Comment(comment);
        }

        public Comment Update(Comment comment)
        {
            return new DbUpdate().Comment(comment);
        }

        public bool Delete(Comment comment)
        {
            return new DbDelete().Comment(comment);
        }
    }
}
