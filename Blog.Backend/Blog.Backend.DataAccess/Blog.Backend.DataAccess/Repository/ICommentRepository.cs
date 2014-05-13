using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IList<Comment> GetTop(Expression<Func<Comment, bool>> predicate, int threshold = 20);
    }
}
