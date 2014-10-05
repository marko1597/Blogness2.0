using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
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
        private readonly IConfigurationHelper _configurationHelper;

        #region Caching variables

        private ICacheDataSource<Post> _cacheDataSource;
        public ICacheDataSource<Post> CacheDataSource
        {
            get { return _cacheDataSource ?? new RedisCache<Post>(_configurationHelper); }
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
            IConfigurationHelper configurationHelper)
        {
            _postsLogic = postsLogic;
            _commentsLogic = commentsLogic;
            _postLikesLogic = postLikesLogic;
            _configurationHelper = configurationHelper;
        }

        public Post GetPost(int postId)
        {
            var post = Cache.GetList(a => a.Id == postId).FirstOrDefault() ?? _postsLogic.GetPost(postId);
            post.PostLikes = _postLikesLogic.Get(post.Id);

            Cache.AddToList(post);

            return post;
        }

        public RelatedPosts GetRelatedPosts(int postId)
        {
            var posts = _postsLogic.GetRelatedPosts(postId);
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

        //private void PushPostProperties(IEnumerable<Post> posts)
        //{
        //    foreach (var post in posts)
        //    {
        //        PushTopComments(post);
        //        PushPostLikes(post);
        //    }
        //}

        //private void PushTopComments(Post post)
        //{
        //    // Push top comments for the post to clients
        //    var topComments = _commentsLogic.GetTopComments(post.Id, 5);
        //    var commentLikesUpdate = new PostComments
        //    {
        //        PostId = post.Id,
        //        Comments = topComments,
        //        ClientFunction = Constants.SocketClientFunctions.GetPostTopComments.ToString()
        //    };
        //    _redisService.Publish(commentLikesUpdate);
        //}

        //private void PushPostLikes(Post post)
        //{
        //    // Push post likes to clients
        //    var postLikes = _postLikesLogic.Get(post.Id);
        //    var postLikesUpdate = new PostLikesUpdate
        //    {
        //        PostId = post.Id,
        //        PostLikes = postLikes,
        //        ClientFunction = Constants.SocketClientFunctions.GetPostLikes.ToString()
        //    };

        //    _redisService.Publish(postLikesUpdate);
        //}
    }
}
