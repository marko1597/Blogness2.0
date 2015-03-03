using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostsResource _postsSvc;
        private readonly IViewCountResource _viewCountSvc;
        private readonly IUsersResource _usersSvc;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IConfigurationHelper _configurationHelper;

        public PostsController(IPostsResource postsSvc, IUsersResource usersSvc, IViewCountResource viewCountSvc, IErrorSignaler errorSignaler, IConfigurationHelper configurationHelper)
        {
            _postsSvc = postsSvc;
            _usersSvc = usersSvc;
            _viewCountSvc = viewCountSvc;
            _errorSignaler = errorSignaler;
            _configurationHelper = configurationHelper;
        }

        [HttpGet]
        [Route("api/posts/{postId:int}")]
        public IHttpActionResult Get(int postId)
        {
            try
            {
                var post = _postsSvc.GetPost(postId) ?? new Post();
                UpdateViewCount(postId);
                
                return Ok(post);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/posts/{postId:int}/related")]
        public IHttpActionResult GetRelated(int postId)
        {
            try
            {
                var posts = _postsSvc.GetRelatedPosts(postId) ?? new RelatedPosts();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/tag/{tagName}")]
        public IHttpActionResult Get(string tagName)
        {
            try
            {
                var posts = _postsSvc.GetPostsByTag(tagName) ?? new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/tag/{tagName}/more/{skip}")]
        public IHttpActionResult GetMoreByTag(string tagName, int skip)
        {
            try
            {
                var posts = _postsSvc.GetMorePostsByTag(tagName, skip) ?? new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/popular")]
        public IHttpActionResult GetPopular()
        {
            try
            {
                var posts = _postsSvc.GetPopularPosts(
                    Convert.ToInt32(_configurationHelper.GetAppSettings("DefaultPostsThreshold"))) ??
                    new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/popular/more/{skip:int?}")]
        public IHttpActionResult GetMorePopular(int skip = 10)
        {
            try
            {
                var posts = _postsSvc.GetMorePopularPosts(
                    Convert.ToInt32(_configurationHelper.GetAppSettings("MorePostsTakeValue")), skip) ??
                    new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/recent")]
        public IHttpActionResult GetRecent()
        {
            try
            {
                var posts = _postsSvc.GetRecentPosts(
                    Convert.ToInt32(_configurationHelper.GetAppSettings("DefaultPostsThreshold"))) ??
                    new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/recent/more/{skip:int?}")]
        public IHttpActionResult GetMoreRecent(int skip = 10)
        {
            try
            {
                var posts = _postsSvc.GetMoreRecentPosts(
                    Convert.ToInt32(_configurationHelper.GetAppSettings("MorePostsTakeValue")), skip) ??
                    new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/posts")]
        public IHttpActionResult GetUserPosts(int userId)
        {
            try
            {
                var posts = _postsSvc.GetPostsByUser(userId) ?? new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/posts/more/{skip}")]
        public IHttpActionResult GetMoreUserPosts(int userId, int skip)
        {
            try
            {
                var posts = _postsSvc.GetMorePostsByUser(userId, skip) ?? new List<Post>();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/posts")]
        public IHttpActionResult Post([FromBody]Post post)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(_postsSvc.AddPost(post));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Post
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = ex.Message
                    }
                };
                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
        [Route("api/posts")]
        public IHttpActionResult Put([FromBody]Post post)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tPost = _postsSvc.GetPost(post.Id);
                var isAllowed = User.Identity.GetUserName() == tPost.User.UserName;

                if (!isAllowed)
                {
                    var notAllowedResult = new Post
                    {
                        Error = new Error
                        {
                            Id = (int)Common.Utils.Constants.Error.RequestNotAllowed,
                            Message = "Request not allowed. You cannot edit someone else's post."
                        }
                    };
                    return Ok(notAllowedResult);
                }

                return Ok(_postsSvc.UpdatePost(post));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Post
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = ex.Message
                    }
                };
                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/posts/{id:int}")]
        public void Delete(int id)
        {
            try
            {
                _postsSvc.DeletePost(id);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        private void UpdateViewCount(int postId)
        {
            try
            {
                int? userId = null;

                var username = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty;
                if (!string.IsNullOrEmpty(username))
                {
                    var user = _usersSvc.GetByUserName(username);
                    if (user != null && user.Error == null)
                    {
                        userId = user.Id;
                    }
                }

                var viewCount = new ViewCount
                {
                    PostId = postId,
                    UserId = userId
                };
                _viewCountSvc.Add(viewCount);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
