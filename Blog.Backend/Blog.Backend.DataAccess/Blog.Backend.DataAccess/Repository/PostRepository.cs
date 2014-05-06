﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Backend.DataAccess.Entities;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Repository
{
    public class PostRepository : GenericRepository<BlogDb, Post>, IPostRepository
    {
        public IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, int threshold = 20)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,Comments,PostLikes").OrderBy(a => a.PostLikes.Count).Take(threshold).ToList();
            return query;
        }

        public IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 20)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,Comments,PostLikes").OrderBy(a => a.CreatedDate).Take(threshold).ToList();
            return query;
        }
    }
}
