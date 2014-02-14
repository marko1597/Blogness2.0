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
            
            return posts;
        }

        public List<Post> PopularPosts(Func<Post, bool> expression, int threshold)
        {
            var db = new Repository<BlogDb>();
            var posts = db.Find(expression, threshold)
                .Select(a => a).OrderBy(a => a.PostLikes.Count).ToList();

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

            return media;
        }

        public List<PostContent> PostContent(Func<PostContent, bool> expression)
        {
            var db = new Repository<BlogDb>();
            var postContent = db.Find(expression)
                .Select(a => a).ToList();

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
