using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.DataAccess.BlogService.Entities;
using Blog.Backend.DataAccess.BlogService.Repository;

namespace Blog.Backend.DataAccess.BlogService.DataAccess
{
    public class DbUpdate
    {
        public Comment Comment(Comment comment)
        {
            var db = new Repository<BlogDb>();
            return db.Update(comment);
        }

        public PostContent PostContent(PostContent postContent)
        {
            var db = new Repository<BlogDb>();
            return db.Update(postContent);
        }

        public MediaGroup MediaGroup(MediaGroup mediaGroup)
        {
            var db = new Repository<BlogDb>();
            return db.Update(mediaGroup);
        }

        public Media Media(Media media)
        {
            var db = new Repository<BlogDb>();
            return db.Update(media);
        }

        public Tag Tag(Tag tag)
        {
            var db = new Repository<BlogDb>();
            return db.Update(tag);
        }

        public Education Education(Education education)
        {
            var db = new Repository<BlogDb>();
            return db.Update(education);
        }

        public Hobby Hobby(Hobby hobby)
        {
            var db = new Repository<BlogDb>();
            return db.Update(hobby);
        }

        public Address Address(Address address)
        {
            var db = new Repository<BlogDb>();
            return db.Update(address);
        }

        public Post Post(Post post)
        {
            var db = new Repository<BlogDb>();
            return db.Update(post);
        }

        public User User(User user)
        {
            var db = new Repository<BlogDb>();
            return db.Update(user);
        }
    }
}
