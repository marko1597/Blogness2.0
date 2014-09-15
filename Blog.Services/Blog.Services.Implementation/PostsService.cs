using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;
 
namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class PostsService : BaseService, IPostsService
    {
        private readonly IPostsLogic _postsLogic;

        public PostsService(IPostsLogic postsLogic)
        {
            _postsLogic = postsLogic;
        }

        public Post GetPost(int postId)
        {
            return _postsLogic.GetPost(postId);
        }

        public RelatedPosts GetRelatedPosts(int postId)
        {
            return _postsLogic.GetRelatedPosts(postId);
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            return _postsLogic.GetPostsByTag(tagName);
        }

        public List<Post> GetMorePostsByTag(string tagName, int skip)
        {
            return _postsLogic.GetMorePostsByTag(tagName, skip);
        }

        public List<Post> GetPostsByUser(int userId)
        {
            return _postsLogic.GetPostsByUser(userId);
        }

        public List<Post> GetMorePostsByUser(int userId, int skip)
        {
            return _postsLogic.GetMorePostsByUser(userId, skip);
        }
        
        public List<Post> GetPopularPosts(int postsCount)
        {
            return _postsLogic.GetPopularPosts(postsCount);
        }

        public List<Post> GetMorePopularPosts(int postsCount, int skip)
        {
            return _postsLogic.GetMorePopularPosts(postsCount, skip);
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            return _postsLogic.GetRecentPosts(postsCount);
        }

        public List<Post> GetMoreRecentPosts(int postsCount, int skip)
        {
            return _postsLogic.GetMoreRecentPosts(postsCount, skip);
        }

        public Post AddPost(Post post)
        {
            return _postsLogic.AddPost(post);
        }

        public Post UpdatePost(Post post)
        {
            return _postsLogic.UpdatePost(post);
        }

        public bool DeletePost(int postId)
        {
            return _postsLogic.DeletePost(postId);
        }
    }
}
