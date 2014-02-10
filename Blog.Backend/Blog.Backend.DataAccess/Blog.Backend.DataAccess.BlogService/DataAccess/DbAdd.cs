using System;
using Blog.Backend.Common.BlogService;
using Blog.Backend.DataAccess.BlogService.Entities;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.DataAccess.BlogService.Repository;

namespace Blog.Backend.DataAccess.BlogService.DataAccess
{
    public class DbAdd
    {
        public Comment Comment(Comment comment)
        {
            var db = new Repository<BlogDb>();
            return db.Add(comment);
        }

        public CommentLike CommentLike(CommentLike commentLike)
        {
            var db = new Repository<BlogDb>();
            return db.Add(commentLike);
        }

        public Tag Tag(Tag tag)
        {
            var db = new Repository<BlogDb>();
            return db.Add(tag);
        }

        public Post Post(Post post)
        {
            var db = new Repository<BlogDb>();

            if (post.Tags != null)
            {
                post.Tags.ForEach(a => PostTag(post.PostId, a));
            }
            return db.Add(post);
        }

        public PostContent PostContent(PostContent postContent)
        {
            var db = new Repository<BlogDb>();
            return db.Add(postContent);
        }

        public MediaGroup MediaGroup(MediaGroup mediaGroup)
        {
            var db = new Repository<BlogDb>();
            return db.Add(mediaGroup);
        }

        public Media Media(Media media)
        {
            var db = new Repository<BlogDb>();
            return db.Add(media);
        }

        public PostLike PostLike(PostLike postLike)
        {
            var db = new Repository<BlogDb>();
            return db.Add(postLike);
        }
        
        public User User(User user)
        {
            var db = new Repository<BlogDb>();

            if (user.Address != null)
            {
                Address(user.Address);
            }
            if (user.Education != null && (user.Education != null || user.Education.Count > 0))
            {
                user.Education.ForEach(a => Education(a));
            }
            if (user.Hobbies != null && (user.Hobbies != null || user.Hobbies.Count > 0))
            {
                user.Hobbies.ForEach(a => Hobby(a));
            }

            return db.Add(user);
        }

        public Session Session(int userId, string ipAddress)
        {
            var db = new Repository<BlogDb>();
            return db.Add(new Session
                              {
                                  Token = Guid.NewGuid().ToString(),
                                  TimeValidity = DateTime.UtcNow.AddHours(Constants.SESSION_VALIDITY_LENGTH),
                                  UserId = userId,
                                  IpAddress = ipAddress
                              });
        }

        public Education Education(Education education)
        {
            var db = new Repository<BlogDb>();
            return db.Add(education);
        }

        public Address Address(Address address)
        {
            var db = new Repository<BlogDb>();
            return db.Add(address);
        }

        public Hobby Hobby(Hobby hobby)
        {
            var db = new Repository<BlogDb>();
            return db.Add(hobby);
        }

        private void PostTag(int postId, Tag tag)
        {
            var db = new Repository<BlogDb>();
            db.Add(new PostTag
            {
                PostId = postId,
                TagId = tag.TagId,
                CreatedBy = tag.CreatedBy,
                CreatedDate = tag.CreatedDate,
                ModifiedBy = tag.ModifiedBy,
                ModifiedDate = tag.ModifiedDate
            });
        }
    }
}
