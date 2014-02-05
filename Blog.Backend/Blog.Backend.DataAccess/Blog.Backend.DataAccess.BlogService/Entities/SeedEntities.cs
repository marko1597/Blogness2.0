using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.DataAccess.BlogService.Entities
{
    public static class SeedEntities
    {
        public static List<Tag> TAGS = new List<Tag>();
        public static List<User> USERS = new List<User>();
        public static List<EducationType> EDUCATIONTYPE = new List<EducationType>();
        public static List<Education> EDUCATION = new List<Education>();
        public static List<Address> ADDRESS = new List<Address>();
        public static List<Hobby> HOBBIES = new List<Hobby>();
        public static List<Post> POSTS = new List<Post>();
        public static List<Comment> COMMENTS = new List<Comment>();
        public static List<Session> SESSIONS = new List<Session>();
        public static List<PostTag> POSTTAGS = new List<PostTag>();
        public static List<PostLike> POSTLIKES = new List<PostLike>();
        public static List<CommentLike> COMMENTLIKES = new List<CommentLike>();
        public static List<PostContent> POSTCONTENT = new List<PostContent>();
        public static List<Media> MEDIA = new List<Media>();
        public static List<MediaGroup> MEDIAGROUP = new List<MediaGroup>();
    }
}
