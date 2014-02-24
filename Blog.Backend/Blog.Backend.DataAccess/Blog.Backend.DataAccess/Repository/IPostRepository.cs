using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Repository
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, bool loadChildren, int threshold = 20);
        IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, bool loadChildren, int threshold = 20);
    }
}
