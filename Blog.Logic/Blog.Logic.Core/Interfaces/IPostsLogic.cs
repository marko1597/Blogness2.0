using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Logic.Core.Interfaces
{
    public interface IPostsLogic
    {
        Post GetPost(int postId);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetMorePostsByTag(string tagName);
        List<Post> GetPostsByUser(int userId);
        List<Post> GetMorePostsByUser(int userId);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetMorePopularPosts(int postsCount, int skip);
        List<Post> GetRecentPosts(int postsCount);
        List<Post> GetMoreRecentPosts(int postsCount, int skip);
        RelatedPosts GetRelatedPosts(int postId);
        Post AddPost(Post post);
        Post UpdatePost(Post post);
        bool DeletePost(int postId);
    }
}
