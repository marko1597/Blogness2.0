using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IPostsResource : IPostsService
    {
    }

    public interface IPostsRestResource
    {
        Post GetPost(int postId);
        RelatedPosts GetRelatedPosts(int postId);
        List<Post> GetPostsByCommunity(int communityId, int threshold = 10, int skip = 10);
        List<Post> GetPostsByTag(string tagName);
        List<Post> GetMorePostsByTag(string tagName, int skip);
        List<Post> GetPostsByUser(int userId);
        List<Post> GetMorePostsByUser(int userId, int skip);
        List<Post> GetPopularPosts(int postsCount);
        List<Post> GetMorePopularPosts(int postsCount, int skip);
        List<Post> GetRecentPosts(int postsCount);
        List<Post> GetMoreRecentPosts(int postsCount, int skip);
        Post AddPost(Post post, string authenticationToken);
        Post UpdatePost(Post post, string authenticationToken);
        bool DeletePost(int postId, string authenticationToken);
    }
}
