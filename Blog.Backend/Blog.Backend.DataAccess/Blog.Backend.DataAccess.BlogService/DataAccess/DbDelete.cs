using System.Linq;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.DataAccess.BlogService.Entities;
using Blog.Backend.DataAccess.BlogService.Repository;

namespace Blog.Backend.DataAccess.BlogService.DataAccess
{
    public class DbDelete
    {
        public bool Comment(Comment comment)
        {
            var dbCommentLikes = new Repository<BlogDb>();
            var commentLikes = dbCommentLikes.Select<CommentLike>()
                .Where(a => a.CommentId == comment.CommentId)
                .Select(a => a).ToList();
            dbCommentLikes.Dispose();
            commentLikes.ForEach(a => CommentLike(a));

            var db = new Repository<BlogDb>();
            return db.Delete(comment);
        }

        public bool CommentLike(CommentLike commentLike)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(commentLike);
        }

        public bool Tag(Tag tag)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(tag);
        }

        public bool MediaGroup(MediaGroup mediaGroup)
        {
            var media = new DbGet().Media(a => a.MediaGroupId == mediaGroup.MediaGroupId).ToList();
            var defaultGroup = new DbGet().MediaGroup(a => a.IsUserDefault && a.UserId == mediaGroup.UserId).First();
            media.ForEach(a =>
                {
                    a.MediaGroupId = defaultGroup.MediaGroupId;
                    new DbUpdate().Media(a);
                });

            var db = new Repository<BlogDb>();
            return db.Delete(mediaGroup);
        }

        public bool Media(Media media)
        {
            var dbPostContent = new Repository<BlogDb>();
            var postContents = dbPostContent.Select<PostContent>()
                .Where(a => a.MediaId == media.MediaId)
                .Select(a => a).ToList();
            dbPostContent.Dispose();
            postContents.ForEach(a => PostContent(a));

            var db = new Repository<BlogDb>();
            return db.Delete(media);
        }

        public bool PostContent(PostContent postContent)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(postContent);
        }

        public bool Post(Post post)
        {
            var db = new Repository<BlogDb>();
            var postTags = db.Select<PostTag>()
                .Where(a => a.PostId == post.PostId)
                .Select(a => a).ToList();
            var postLikes = db.Select<PostLike>()
                .Where(a => a.PostId == post.PostId)
                .Select(a => a).ToList();
            var comments = db.Select<Comment>()
                .Where(a => a.PostId == post.PostId)
                .Select(a => a).ToList();
            var postContents = db.Select<PostContent>()
                .Where(a => a.PostId == post.PostId)
                .Select(a => a).ToList();
            db.Dispose();

            postTags.ForEach(PostTag);
            postLikes.ForEach(a => PostLike(a));
            comments.ForEach(a => Comment(a));
            postContents.ForEach(a => PostContent(a));

            var dbDelete = new Repository<BlogDb>();
            post.Tags = null;
            post.Comments = null;
            post.PostContents = null;
            post.PostLikes = null;
            return dbDelete.Delete(post);
        }

        public bool PostLike(PostLike postLike)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(postLike);
        }

        public bool User(User user)
        {
            var db = new Repository<BlogDb>();
            var education = db.Select<Education>()
                .Where(a => a.UserId == user.UserId)
                .Select(a => a).ToList();
            var hobbies = db.Select<Hobby>()
                .Where(a => a.UserId == user.UserId)
                .Select(a => a).ToList();
            var address = db.Select<Address>()
                .FirstOrDefault(a => a.UserId == user.UserId);
            db.Dispose();

            education.ForEach(a => Education(a));
            hobbies.ForEach(a => Hobby(a));
            Address(address);

            var dbDelete = new Repository<BlogDb>();
            user.Education = null;
            user.Hobbies = null;
            user.Address = null;
            return dbDelete.Delete(user);
        }

        public bool Education(Education education)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(education);
        }

        public bool Address(Address address)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(address);
        }


        public bool Hobby(Hobby hobby)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(hobby);
        }

        public bool Session(Session session)
        {
            var db = new Repository<BlogDb>();
            return db.Delete(session);
        }

        private void PostTag(PostTag postTag)
        {
            var db = new Repository<BlogDb>();
            db.Delete(postTag);
        }
    }
}
