using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IPostsPage
    {
        UserPosts GetUserPosts(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetRecentPosts(int postsCount);
    }
}
