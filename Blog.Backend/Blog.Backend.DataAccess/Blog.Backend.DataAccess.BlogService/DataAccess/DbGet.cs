using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.DataAccess.BlogService.Entities;
using Blog.Backend.DataAccess.BlogService.Repository;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.DataAccess.BlogService.DataAccess
{
    public class DbGet
    {
        public List<Tag> Tags(int postId)
        {
            var db = new Repository<BlogDb>();
            var tagIds = db.Select<PostTag>()
                .Where(a => a.PostId == postId)
                .Select(a => a.TagId).ToList();
            var tags = db.Select<Tag>()
                .Where(a => tagIds.Contains(a.TagId))
                .Select(a => a)
                .ToList();
            return tags;
        }

        public List<Tag> Tags(Func<Tag, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var tags = db.Find(expression)
                .Select(a => a).ToList();
               
            return tags;
        }

        public List<Comment> Comments(Func<Comment, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var comments = db.Find(expression)
                .Select(a => a).ToList();

            return comments;
        }

        public List<Comment> Comments(Func<Comment, bool> expression, bool isComplete)
        {
            var db = new Repository<BlogDb>();
            var comments = db.Find(expression)
                .Select(a => a).ToList();

            foreach (var c in comments)
            {
                var c1 = c;
                c.User = Users(a => a.UserId == c1.UserId).First();
                var c2 = c;
                c.Comments = Comments(a => a.ParentCommentId == c2.CommentId).Select(a => a).ToList();
                var c3 = c;
                c.CommentLikes = CommentLikes(a => a.CommentId == c3.CommentId);
            }

            return comments;
        }

        public List<CommentLike> CommentLikes(Func<CommentLike, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<Post> Posts(Func<Post, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var posts = db.Find(expression)
                .Select(a => a).ToList();

            foreach (var post in posts)
            {
                post.Tags = Tags(post.PostId);
                var post1 = post;
                post.PostLikes = PostLikes(a => a.PostId == post1.PostId);
                var post2 = post;
                post.User = Users(a => a.UserId == post2.UserId).First();
            }

            return posts;
        }

        public List<Post> Posts(Func<Post, bool> expression, int threshold)
        {
            var db = new Repository<BlogDb>();
            var posts = db.Find(expression, threshold)
                .Select(a => a).ToList();

            foreach (var post in posts)
            {
                post.Tags = Tags(post.PostId);
                var post1 = post;
                post.PostLikes = PostLikes(a => a.PostId == post1.PostId);
                var post2 = post;
                post.User = Users(a => a.UserId == post2.UserId).First();
            }

            return posts;
        }

        public List<MediaGroup> MediaGroup(Func<MediaGroup, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<Media> Media(Func<Media, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var media = db.Find(expression)
                .Select(a => a).ToList();

            foreach (var m in media)
            {
                var m1 = m;
                m.MediaGroup = MediaGroup(a => a.MediaGroupId == m1.MediaGroupId).First();
            }

            return media;
        }

        public List<PostContent> PostContent(Func<PostContent, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var postContent = db.Find(expression)
                .Select(a => a).ToList();

            postContent.ForEach(a =>
                {
                    a.Media = Media(m => m.MediaId == a.MediaId).FirstOrDefault();
                });

            return postContent;
        }

        public List<PostLike> PostLikes(Func<PostLike, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<Hobby> Hobbies(Func<Hobby, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<Address> Address(Func<Address, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<Education> Education(Func<Education, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<User> Users(Func<User, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var users = db.Find(expression)
                    .Select(a => a).ToList();

            return users;
        }

        public List<User> Users(Func<User, bool> expression, bool isComplete)
        {
            var db = new Repository<BlogDb>();
            var users = db.Find(expression)
                    .Select(a => a).ToList();

            if (isComplete)
            {
                users.ForEach(a =>
                    {
                        a.Address = Address(d => d.UserId == a.UserId).First();
                        a.Education = Education(e => e.UserId == a.UserId);
                        a.Hobbies = Hobbies(h => h.UserId == a.UserId);
                    });
            }
            
            return users;
        }

        public List<PostTag> PostTags(Func<PostTag, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }

        public List<Session> Sessions(Func<Session, bool> expression)
        {
            var db = new Repository<BlogDb>();
            return db.Find(expression).
                Select(a => a).ToList();
        }
    }
}
