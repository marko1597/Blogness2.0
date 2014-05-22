using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
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
