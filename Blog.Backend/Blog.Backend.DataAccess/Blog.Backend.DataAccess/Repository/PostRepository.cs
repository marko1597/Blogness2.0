using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 20)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public override Post Add(Post post)
        {
            if (post.Tags != null)
            {
                var tags = post.Tags;
                post.Tags = new List<Tag>();

                foreach (var t in tags)
                {
                    var t1 = t;
                    var q = Context.Tags.Where(a => a.TagName.ToLower() == t1.TagName.ToLower()).ToList();

                    if (q.Count == 0)
                    {
                        Context.Tags.Attach(t);
                        Context.Entry(t).State = EntityState.Added;
                        post.Tags.Add(t);
                    }
                    else
                    {
                        Context.Tags.Attach(q.FirstOrDefault());
                        Context.Entry(q.FirstOrDefault()).State = EntityState.Unchanged;
                        post.Tags.Add(q.FirstOrDefault());
                    }
                }
            }

            if (post.PostContents != null)
            {
                var contents = post.PostContents;
                post.PostContents = new List<PostContent>();

                foreach (var postContent in contents)
                {
                    post.PostContents.Add(postContent);
                }
            }

            Context.Entry(post).State = EntityState.Added;
            Context.SaveChanges();

            return post;
        }
    }
}
