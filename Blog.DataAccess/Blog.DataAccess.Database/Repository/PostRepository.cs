﻿using System;
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
            var query = Find(a => a.Tags.Any(t => t.TagName == tagName), null, "Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePostsByTag(string tagName, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.Tags.Any(t => t.TagName == tagName), null, "Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, int threshold = 10)
        {
            var query = Find(predicate, null, "Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePopularPosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10)
        {
            var query = Find(predicate, null, "Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 10)
        {
            var query = Find(predicate, null, "Tags,User")
                .OrderByDescending(a => a.ModifiedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMoreRecentPosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10)
        {
            var query = Find(predicate, null, "Tags,User")
                .OrderByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetByUser(int userId, int threshold = 10)
        {
            var query = Find(a => a.UserId == userId, null, "Tags,User")
                .OrderByDescending(a => a.ModifiedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePostsByUser(int userId, int threshold = 5, int skip = 10)
        {
            var query = Find(a => a.UserId == userId, null, "Tags,User")
                .OrderByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetByCommunity(int communityId, int threshold = 10, int skip = 10)
        {
            var query = Find(a => a.Communities.Any(c => c.Id == communityId), null, "Tags,User")
                .Distinct()
                .OrderByDescending(a => a.ModifiedDate)
                .Take(threshold)
                .Skip(skip)
                .ToList();

            foreach (var post in query)
            {
                post.Communities = null;
            }

            return query;
        }

        public override Post Add(Post post)
        {
            #region Tags
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
            #endregion

            #region Contents
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
            #endregion

            #region Communities
            // Add communities included in this entity to DB
            if (post.Communities != null)
            {
                var communities = post.Communities;
                post.Communities = new List<Community>();

                foreach (var q in communities.Select(c => Context.Communities.Where(a =>
                    a.Id == c.Id).ToList()).Where(q => q.Count > 0))
                {
                    Context.Communities.Attach(q.FirstOrDefault());
                    Context.Entry(q.FirstOrDefault()).State = EntityState.Unchanged;
                    post.Communities.Add(q.FirstOrDefault());
                }
            }
            #endregion

            post.CreatedDate = DateTime.Now;
            post.CreatedBy = post.UserId;
            post.ModifiedDate = DateTime.Now;
            post.ModifiedBy = post.UserId;

            Context.Entry(post).State = EntityState.Added;
            Context.SaveChanges();
            Context.Entry(post).State = EntityState.Detached;

            return post;
        }

        public override Post Edit(Post post)
        {
            var db = Context.Posts
                .Include(a => a.PostContents)
                .Include(a => a.PostLikes)
                .Include(a => a.Tags)
                .Include(a => a.Comments)
                .Include(a => a.Communities)
                .FirstOrDefault(a => a.PostId == post.PostId);

            if (db == null) throw new Exception("Record not found!");

            db.PostTitle = post.PostTitle;
            db.PostMessage = post.PostMessage;

            #region Tags

            var tempTags = db.Tags.ToList();
            foreach (var t in tempTags)
            {
                Context.Entry(t).State = GetTagState(t, post.Tags);
            }

            var newtags = RepositoryHelper.GetNewTags(db.Tags, post.Tags, post.UserId);
            newtags.ForEach(a =>
            {
                Context.Tags.Attach(a);
                Context.Entry(a).State = EntityState.Added;
                db.Tags.Add(a);
            });

            #endregion

            #region Contents

            var tempContents = db.PostContents.ToList();
            foreach (var c in tempContents)
            {
                var tContent = post.PostContents
                    .FirstOrDefault(a => a.PostId == c.PostId && a.MediaId == c.MediaId);

                if (tContent != null)
                {
                    var tmpContent = post.PostContents
                        .FirstOrDefault(a => a.PostId == c.PostId && a.MediaId == c.MediaId);
                    if (tmpContent != null)
                    {
                        c.PostContentTitle = tmpContent.PostContentTitle;
                        c.PostContentText = tmpContent.PostContentText;
                    }

                    Context.Entry(c).State = EntityState.Modified;
                }
                else
                {
                    Context.Entry(c).State = EntityState.Deleted;
                }
            }

            var newcontents = RepositoryHelper.GetNewContents(db.PostContents, post.PostContents, post.UserId);
            newcontents.ForEach(a =>
            {
                Context.PostContents.Attach(a);
                Context.Entry(a).State = EntityState.Added;
                db.PostContents.Add(a);
            });

            #endregion

            #region Communities

            if (post.Communities != null)
            {
                var tempCommunities = db.Communities.ToList();
                foreach (var t in tempCommunities)
                {
                    Context.Entry(t).State = GetCommunityState(t, post.Communities);
                }
            }

            #endregion

            db.ModifiedDate = DateTime.Now;
            db.ModifiedBy = post.UserId;

            Context.Entry(db).State = EntityState.Modified;
            Context.SaveChanges();
            Context.Entry(post).State = EntityState.Detached;

            return post;
        }

        #region Private methods

        private static EntityState GetTagState(Tag tag, IEnumerable<Tag> tags)
        {
            var tagNames = tags.Select(a => a.TagName.ToLower()).ToList();

            return tagNames.Contains(tag.TagName.ToLower()) ?
                EntityState.Unchanged : EntityState.Deleted;
        }

        private static EntityState GetCommunityState(Community community, IEnumerable<Community> communities)
        {
            var communityIds = communities.Select(a => a.Id).ToList();

            return communityIds.Contains(community.Id) ? EntityState.Unchanged : EntityState.Deleted;
        }

        #endregion
    }
}
