using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public static class DataStorage
    {
        public static List<Address> Addresses = new List<Address>();
        public static List<Album> Albums = new List<Album>();
        public static List<Comment> Comments = new List<Comment>();
        public static List<CommentLike> CommentLikes = new List<CommentLike>();
        public static List<Education> Educations = new List<Education>();
        public static List<EducationType> EducationTypes = new List<EducationType>();
        public static List<Hobby> Hobbies = new List<Hobby>();
        public static List<Media> Media = new List<Media>();
        public static List<Post> Posts = new List<Post>();
        public static List<PostContent> PostContents = new List<PostContent>();
        public static List<PostLike> PostLikes = new List<PostLike>();
        public static List<Session> Sessions = new List<Session>();
        public static List<Tag> Tags = new List<Tag>();
        public static List<PostTag> PostTags = new List<PostTag>();
        public static List<User> Users = new List<User>();
    }
}
