using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Logic.Caching;
using Blog.Logic.Caching.DataSource;
using Blog.Logic.Caching.DataSource.Redis;
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
        private readonly ICommentsLogic _commentsLogic;
        private readonly IPostLikesLogic _postLikesLogic;
        private readonly IViewCountLogic _viewCountLogic;
        private readonly IConfigurationHelper _configurationHelper;
        private readonly IErrorSignaler _errorSignaler;

        #region Caching variables

        private ICacheDataSource<Post> _cacheDataSource;
        public ICacheDataSource<Post> CacheDataSource
        {
            get { return _cacheDataSource ?? new RedisCache<Post>(_configurationHelper, _errorSignaler); }
            set { _cacheDataSource = value; }
        }

        private ICache<Post> _cache;
        public ICache<Post> Cache
        {
            get { return _cache ?? new Cache<Post>(CacheDataSource); }
            set { _cache = value; }
        }

        #endregion

        public PostsService(IPostsLogic postsLogic, ICommentsLogic commentsLogic, IPostLikesLogic postLikesLogic,
            IViewCountLogic viewCountLogic, IConfigurationHelper configurationHelper, IErrorSignaler errorSignaler)
        {
            _postsLogic = postsLogic;
            _commentsLogic = commentsLogic;
            _postLikesLogic = postLikesLogic;
            _viewCountLogic = viewCountLogic;
            _configurationHelper = configurationHelper;
            _errorSignaler = errorSignaler;
        }

        public Post GetPost(int postId)
        {
            var cache = Cache.GetEntry(postId);

            if (cache != null)
            {
                cache.PostLikes = _postLikesLogic.Get(cache.Id);
                return cache;
            }

            var post = _postsLogic.GetPost(postId);

            Cache.SetEntry(post, postId);

            post.PostLikes = _postLikesLogic.Get(post.Id);
            post.ViewCounts = _viewCountLogic.Get(post.Id);

            return post;
        }

        public RelatedPosts GetRelatedPosts(int postId)
        {
            var posts = _postsLogic.GetRelatedPosts(postId);
            return posts;
        }

        public List<Post> GetPostsByCommunity(int communityId, int threshold = 10, int skip = 10)
        {
            var dbPosts = _postsLogic.GetPostsByCommunity(communityId, threshold, skip);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            var dbPosts = _postsLogic.GetPostsByTag(tagName);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetMorePostsByTag(string tagName, int skip)
        {
            var dbPosts = _postsLogic.GetMorePostsByTag(tagName, skip);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetPostsByUser(int userId)
        {
            var dbPosts = _postsLogic.GetPostsByUser(userId);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetMorePostsByUser(int userId, int skip)
        {
            var dbPosts = _postsLogic.GetMorePostsByUser(userId, skip);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            var dbPosts = _postsLogic.GetPopularPosts(postsCount);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetMorePopularPosts(int postsCount, int skip)
        {
            var dbPosts = _postsLogic.GetMorePopularPosts(postsCount, skip);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            var dbPosts = _postsLogic.GetRecentPosts(postsCount);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public List<Post> GetMoreRecentPosts(int postsCount, int skip)
        {
            var dbPosts = _postsLogic.GetMoreRecentPosts(postsCount, skip);
            var posts = SetPostProperties(dbPosts);

            return posts;
        }

        public Post AddPost(Post post)
        {
            var result = _postsLogic.AddPost(post);
            Cache.SetEntry(result, result.Id);

            return result;
        }

        public Post UpdatePost(Post post)
        {
            var result = _postsLogic.UpdatePost(post);
            Cache.ReplaceEntry(result, result.Id);

            return result;
        }

        public bool DeletePost(int postId)
        {
            var result = _postsLogic.DeletePost(postId);
            Cache.RemoveEntry(postId);

            return result;
        }

        private List<Post> SetPostProperties(IEnumerable<Post> posts)
        {
            var tmpPosts = new List<Post>();

            foreach (var post in posts)
            {
                post.Comments = _commentsLogic.GetTopComments(post.Id, 5);
                post.PostLikes = _postLikesLogic.Get(post.Id);
                tmpPosts.Add(post);
            }

            return tmpPosts;
        }
    }
}
