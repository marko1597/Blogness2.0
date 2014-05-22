using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation
{
    public interface IPostsPage
    {
        UserPosts GetUserPosts(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetRecentPosts(int postsCount);
        List<Post> GetMorePosts(int postsCount, int skip);
    }
}
