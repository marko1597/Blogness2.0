using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    [ExcludeFromCodeCoverage]
    public class PostRepository : GenericRepository<BlogDb, Post>, IPostRepository
    {
        public IList<Post> GetPostsByTag(string tagName, int threshold = 10)
        {
            var query = Find(a => a.Tags.Any(t => t.TagName == tagName), null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePostsByTag(string tagName, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.Tags.Any(t => t.TagName == tagName), null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, int threshold = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePopularPosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.ModifiedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMoreRecentPosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetByUser(int userId, int threshold = 10)
        {
            var query = Find(a => a.UserId == userId, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.ModifiedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePostsByUser(int userId, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.UserId == userId, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public override Post Add(Post post)
        {
            // Add tags included in this entity to DB
            if (post.Tags != null)
            {
                var tags = post.Tags;
                post.Tags = new List<Tag>();

                foreach (var t in tags)
                {
                    var q = Context.Tags.Where(a => a.TagName.ToLower() == t.TagName.ToLower()).ToList();

                    if (q.Count == 0)
                    {
                        t.CreatedDate = DateTime.Now;
                        t.CreatedBy = post.UserId;
                        t.ModifiedDate = DateTime.Now;
                        t.ModifiedBy = post.UserId;

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

            // Add post contents included in this entity to DB
            if (post.PostContents != null)
            {
                var contents = post.PostContents;
                post.PostContents = new List<PostContent>();

                foreach (var postContent in contents)
                {
                    postContent.CreatedDate = DateTime.Now;
                    postContent.CreatedBy = post.UserId;
                    postContent.ModifiedDate = DateTime.Now;
                    postContent.ModifiedBy = post.UserId;

                    post.PostContents.Add(postContent);
                }
            }

            Context.Entry(post).State = EntityState.Added;
            Context.SaveChanges();

            return post;
        }

        public override Post Edit(Post post)
        {
            var db = Context.Posts
                .Include(a => a.PostContents)
                .Include(a => a.PostLikes)
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .FirstOrDefault(a => a.PostId == post.PostId);

            if (db != null)
            {
                db.PostTitle = post.PostTitle;
                db.PostMessage = post.PostMessage;

                var tempTags = db.Tags.ToList();
                foreach (var t in tempTags)
                {
                    Context.Entry(t).State = GetTagState(t, post.Tags);
                }

                var newtags = GetNewTags(db.Tags, post.Tags);
                newtags.ForEach(a =>
                                {
                                    Context.Tags.Attach(a);
                                    Context.Entry(a).State = EntityState.Added;
                                    db.Tags.Add(a);
                                });

                var tempContents = db.PostContents.ToList();
                foreach (var c in tempContents)
                {
                    Context.Entry(c).State = GetContentState(c, post.PostContents);
                }

                var newcontents = GetNewContents(db.PostContents, post.PostContents);
                newcontents.ForEach(a =>
                {
                    Context.PostContents.Attach(a);
                    Context.Entry(a).State = EntityState.Added;
                    db.PostContents.Add(a);
                });
            }

            Context.Entry(db).State = EntityState.Modified;
            Context.SaveChanges();

            return post;
        }

        #region Private methods

        private EntityState GetTagState(Tag tag, IEnumerable<Tag> tags)
        {
            var tagNames = tags.Select(a => a.TagName.ToLower()).ToList();

            return tagNames.Contains(tag.TagName.ToLower()) ?
                EntityState.Unchanged : EntityState.Deleted;
        }

        private List<Tag> GetNewTags(IEnumerable<Tag> dbTags, IEnumerable<Tag> clientTags)
        {
            var dbTagNames = dbTags.Select(a => a.TagName.ToLower()).ToList();
            var newTags = (from t in clientTags
                           where dbTagNames.All(a => a != t.TagName)
                           select t).ToList();

            foreach(var t in newTags)
            {
                t.CreatedDate = DateTime.Now;
                t.CreatedBy = post.UserId;
                t.ModifiedDate = DateTime.Now;
                t.ModifiedBy = post.UserId;
            }

            return newTags;
        }

        private EntityState GetContentState(PostContent content, IEnumerable<PostContent> contents)
        {
            var tContent = contents.Where(a => a.PostId == content.PostId && a.MediaId == content.MediaId).ToList();
            return tContent.Count > 0 ? EntityState.Unchanged : EntityState.Deleted;
        }

        private List<PostContent> GetNewContents(IEnumerable<PostContent> dbContents, IEnumerable<PostContent> clientContents)
        {
            var newContents = (from c in clientContents
                               where dbContents.All(a => a.MediaId != c.MediaId)
                               select c).ToList();

            foreach (var pc in newContents)
            {
                pc.CreatedDate = DateTime.Now;
                pc.CreatedBy = post.UserId;
                pc.ModifiedDate = DateTime.Now;
                pc.ModifiedBy = post.UserId;
            }

            return newContents;
        }

        #endregion
    }
}
