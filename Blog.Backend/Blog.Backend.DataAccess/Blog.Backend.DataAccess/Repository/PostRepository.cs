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
        public IList<Post> GetPopular(Expression<Func<Post, bool>> predicate, int threshold = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.PostLikes.Count)
                .ThenByDescending(b => b.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetRecent(Expression<Func<Post, bool>> predicate, int threshold = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(a => a.CreatedDate)
                .Take(threshold)
                .ToList();
            return query;
        }

        public IList<Post> GetMorePosts(Expression<Func<Post, bool>> predicate, int threshold = 5, int skip = 10)
        {
            var query = Find(predicate, null, "PostContents,Tags,User,PostLikes")
                .OrderByDescending(b => b.CreatedDate)
                .Skip(skip)
                .Take(threshold)
                .ToList();
            return query;
        }

        public Post Save(Post post, bool isAdding)
        {
            if (!isAdding)
            {
                Context.Posts.Attach(post);
            }

            #region Tags

            if (post.Tags != null)
            {
                var tags = post.Tags;
                var tagIds = new List<int>();
                post.Tags = new List<Tag>();

                foreach (var t in tags)
                {
                    tagIds.Add(t.TagId);
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

                if (!isAdding)
                {
                    var tPost = Context.Posts.Include("Tags")
                        .FirstOrDefault(p => p.PostId == post.PostId);

                    if (tPost != null)
                    {
                        var deletedTags = (from t in tPost.Tags
                            where tagIds.All(a => a != t.TagId)
                            select t).ToList();

                        deletedTags.ForEach(a =>
                                            {
                                                Context.Tags.Attach(a);
                                                Context.Entry(a).State = EntityState.Deleted;
                                                post.Tags.Add(a);
                                                post.Tags.Remove(a);
                                            });
                    }
                }
            }

            #endregion

            #region Contents

            if (post.PostContents != null)
            {
                if (isAdding)
                {
                    var contents = post.PostContents;
                    post.PostContents = new List<PostContent>();

                    foreach (var postContent in contents)
                    {
                        post.PostContents.Add(postContent);
                    }
                }
                else
                {
                    foreach (var c in post.PostContents)
                    {
                        var c1 = c;
                        var q = Context.PostContents
                            .Where(a => a.PostId == c1.PostId && a.MediaId == c1.MediaId).ToList();

                        Context.Entry(c).State = q.Count == 0 ? EntityState.Added : EntityState.Unchanged;
                    }
                }
            }

            #endregion

            if (!isAdding)
            {
                post.Comments = new List<Comment>();
                var comments = Context.Comments.Where(a => a.PostId == post.PostId).ToList();
                comments.ForEach(a =>
                                 {
                                     Context.Comments.Attach(a);
                                     Context.Entry(a).State = EntityState.Unchanged;
                                     post.Comments.Add(a);
                                 });

                post.PostLikes = new List<PostLike>();
                var likes = Context.PostLikes.Where(a => a.PostId == post.PostId).ToList();
                likes.ForEach(a =>
                {
                    Context.PostLikes.Attach(a);
                    Context.Entry(a).State = EntityState.Unchanged;
                    post.PostLikes.Add(a);
                });
            }

            Context.Entry(post).State = isAdding ? EntityState.Added : EntityState.Modified;
            Context.SaveChanges();

            return post;
        }

        //public override Post Edit(Post post)
        //{
        //    Context.Posts.Attach(post);
            
        //    #region Tags

        //    if (post.Tags != null)
        //    {
        //        var tags = post.Tags;
        //        var tagIds = new List<int>();
        //        post.Tags = new List<Tag>();

                

        //        if (!isAdding)
        //        {
        //            var tPost = Context.Posts.Include("Tags")
        //                .FirstOrDefault(p => p.PostId == post.PostId);

        //            if (tPost != null)
        //            {
        //                var deletedTags = (from t in tPost.Tags
        //                                   where tagIds.All(a => a != t.TagId)
        //                                   select t).ToList();

        //                deletedTags.ForEach(a =>
        //                {
        //                    Context.Tags.Attach(a);
        //                    Context.Entry(a).State = EntityState.Deleted;
        //                    post.Tags.Add(a);
        //                    post.Tags.Remove(a);
        //                });
        //            }
        //        }
        //    }

        //    #endregion

        //    #region Contents

        //    if (post.PostContents != null)
        //    {
        //        if (isAdding)
        //        {
        //            var contents = post.PostContents;
        //            post.PostContents = new List<PostContent>();

        //            foreach (var postContent in contents)
        //            {
        //                post.PostContents.Add(postContent);
        //            }
        //        }
        //        else
        //        {
        //            foreach (var c in post.PostContents)
        //            {
        //                var c1 = c;
        //                var q = Context.PostContents
        //                    .Where(a => a.PostId == c1.PostId && a.MediaId == c1.MediaId).ToList();

        //                Context.Entry(c).State = q.Count == 0 ? EntityState.Added : EntityState.Unchanged;
        //            }
        //        }
        //    }

        //    #endregion

        //    if (!isAdding)
        //    {
        //        post.Comments = new List<Comment>();
        //        var comments = Context.Comments.Where(a => a.PostId == post.PostId).ToList();
        //        comments.ForEach(a =>
        //        {
        //            Context.Comments.Attach(a);
        //            Context.Entry(a).State = EntityState.Unchanged;
        //            post.Comments.Add(a);
        //        });

        //        post.PostLikes = new List<PostLike>();
        //        var likes = Context.PostLikes.Where(a => a.PostId == post.PostId).ToList();
        //        likes.ForEach(a =>
        //        {
        //            Context.PostLikes.Attach(a);
        //            Context.Entry(a).State = EntityState.Unchanged;
        //            post.PostLikes.Add(a);
        //        });
        //    }

        //    Context.Entry(post).State = isAdding ? EntityState.Added : EntityState.Modified;
        //    Context.SaveChanges();

        //    return post;
        //}

        //private List<Tag> UpdateTagsContext(ref Post post, IList<Tag> tags)
        //{
        //    foreach (var t in tags)
        //    {
        //        tagIds.Add(t.TagId);
        //        var t1 = t;
        //        var q = Context.Tags.Where(a => a.TagName.ToLower() == t1.TagName.ToLower()).ToList();

        //        if (q.Count == 0)
        //        {
        //            Context.Tags.Attach(t);
        //            Context.Entry(t).State = EntityState.Added;
        //            post.Tags.Add(t);
        //        }
        //        else
        //        {
        //            Context.Tags.Attach(q.FirstOrDefault());
        //            Context.Entry(q.FirstOrDefault()).State = EntityState.Unchanged;
        //            post.Tags.Add(q.FirstOrDefault());
        //        }
        //    }
        //}
    }
}
