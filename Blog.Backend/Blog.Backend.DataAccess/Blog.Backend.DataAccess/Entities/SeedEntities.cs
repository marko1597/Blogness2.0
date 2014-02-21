using System.Collections.Generic;
using Blog.Backend.DataAccess.Entities.Objects;

namespace Blog.Backend.DataAccess.Entities
{
    public static class SeedEntities
    {
        public static List<Tag> Tags = new List<Tag>();
        public static List<User> Users = new List<User>();
        public static List<EducationType> Educationtype = new List<EducationType>();
        public static List<Education> Education = new List<Education>();
        public static List<Address> Address = new List<Address>();
        public static List<Hobby> Hobbies = new List<Hobby>();
        public static List<Post> Posts = new List<Post>();
        public static List<Comment> Comments = new List<Comment>();
        public static List<Session> Sessions = new List<Session>();
        public static List<PostLike> PostLikes = new List<PostLike>();
        public static List<CommentLike> CommentLikes = new List<CommentLike>();
        public static List<PostContent> PostContents = new List<PostContent>();
        public static List<Media> Media = new List<Media>();
        public static List<MediaGroup> MediaGroups = new List<MediaGroup>();
    }
}
