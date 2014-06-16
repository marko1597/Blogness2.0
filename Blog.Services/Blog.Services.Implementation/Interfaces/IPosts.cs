using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IPosts
    {
        Post GetPost(int postId);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetPostsByUser(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetRecentPosts(int postsCount);
        List<Post> GetMorePosts(int postsCount, int skip);
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        bool DeletePost(int postId);
    }
}
