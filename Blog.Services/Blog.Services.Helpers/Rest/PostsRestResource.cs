using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class PostsRestResource : IPostsRestResource
    {
        public Post GetPost(int postId)
        {
            throw new System.NotImplementedException();
        }

        public RelatedPosts GetRelatedPosts(int postId)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetPostsByCommunity(int communityId, int threshold = 10, int skip = 10)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetMorePostsByTag(string tagName, int skip)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetPostsByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetMorePostsByUser(int userId, int skip)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetMorePopularPosts(int postsCount, int skip)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            throw new System.NotImplementedException();
        }

        public List<Post> GetMoreRecentPosts(int postsCount, int skip)
        {
            throw new System.NotImplementedException();
        }

        public Post AddPost(Post post, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public Post UpdatePost(Post post, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool DeletePost(int postId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
