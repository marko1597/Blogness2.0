using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        IList<Post> GetPostsByTag(string tagName, int threshold = 10);
        IList<Post> GetMorePostsByTag(string tagName, int threshold = 5, int skip = 10);
        IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, int threshold = 10);
        IList<Post> GetMorePopularPosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10);
        IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 10);
        IList<Post> GetMoreRecentPosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10);
        IList<Post> GetByCommunity(int communityId, int threshold = 10, int skip = 10);
        IList<Post> GetByUser(int userId, int threshold = 10);
        IList<Post> GetMorePostsByUser(int userId, int threshold = 5, int skip = 10);
    }
}
