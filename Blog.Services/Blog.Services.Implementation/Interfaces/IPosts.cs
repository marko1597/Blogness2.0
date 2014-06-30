using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IPosts
    {
        Post GetPost(int postId);
        RelatedPosts GetRelatedPosts(int postId);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetMorePostsByTag(string tagName);
        List<Post> GetPostsByUser(int userId);
        List<Post> GetMorePostsByUser(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetMorePopularPosts(int postsCount, int skip);
        List<Post> GetRecentPosts(int postsCount);
        List<Post> GetMoreRecentPosts(int postsCount, int skip);
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        bool DeletePost(int postId);
    }
}
