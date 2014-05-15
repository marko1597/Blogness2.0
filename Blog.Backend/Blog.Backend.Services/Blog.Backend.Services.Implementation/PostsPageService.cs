using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
{
    public class PostsPageService : IPostsPage
    {
        public UserPosts GetUserPosts(int userId)
        {
            return PostsPageFactory.GetInstance().CreatePostsPage().GetUserPosts(userId);
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            return PostsPageFactory.GetInstance().CreatePostsPage().GetPopularPosts(postsCount);
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            return PostsPageFactory.GetInstance().CreatePostsPage().GetRecentPosts(postsCount);
        }

        public List<Post> GetMorePosts(int postsCount, int skip)
        {
            return PostsPageFactory.GetInstance().CreatePostsPage().GetMorePosts(postsCount, skip);
        }
    }
}
