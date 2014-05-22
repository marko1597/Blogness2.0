using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IList<Comment> GetTop(Expression<Func<Comment, bool>> predicate, int threshold = 20);
    }
}
