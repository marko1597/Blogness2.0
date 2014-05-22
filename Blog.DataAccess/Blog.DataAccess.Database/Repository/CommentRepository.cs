using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository
{
    public class CommentRepository : GenericRepository<BlogDb, Comment>, ICommentRepository
    {
        public IList<Comment> GetTop(Expression<Func<Comment, bool>> predicate, int threshold = 20)
        {
            var query = Find(predicate, null, "CommentLikes,User,ParentComment")
                .OrderByDescending(a => a.CommentLikes.Count)
                .ThenByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }
    }
}
