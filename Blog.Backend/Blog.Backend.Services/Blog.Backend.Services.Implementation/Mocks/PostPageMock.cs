using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class PostPageMock : IPostsPage
    {
        public UserPosts GetUserPosts(int userId)
        {
            var posts = DataStorage.Posts.FindAll(a => a.User.UserId == userId);
            var user = DataStorage.Users.FirstOrDefault(a => a.UserId == userId);
            return new UserPosts {Posts = posts, User = user};
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            var posts = DataStorage.Posts
                .OrderByDescending(a => a.PostLikes.Count)
                .Take(postsCount)
                .ToList();
            return posts;
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            var posts = DataStorage.Posts
                .OrderByDescending(a => a.CreatedDate)
                .Take(postsCount)
                .ToList();
            return posts;
        }
    }
}
