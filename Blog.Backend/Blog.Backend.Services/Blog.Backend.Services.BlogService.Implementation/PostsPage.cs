using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class PostsPage : IPostsPage
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
    }
}
