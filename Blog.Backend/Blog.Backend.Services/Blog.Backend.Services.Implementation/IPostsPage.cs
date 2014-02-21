using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;

namespace Blog.Backend.Services.Implementation
{
    public interface IPostsPage
    {
        UserPosts GetUserPosts(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetRecentPosts(int postsCount);
    }
}
