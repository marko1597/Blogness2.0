using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Repository
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, int threshold = 10);
        IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 10);
        IList<Post> GetMorePosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10);
        Post Save(Post post, bool isAdding);
    }
}
