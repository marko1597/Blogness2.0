using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPostsResource _postsSvc;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IConfigurationHelper _configurationHelper;

        public PostsController(IPostsResource postsSvc, IErrorSignaler errorSignaler, IConfigurationHelper configurationHelper)
        {
            _postsSvc = postsSvc;
            _errorSignaler = errorSignaler;
            _configurationHelper = configurationHelper;
        }

        [HttpGet]
        [Route("api/posts/{postId:int}")]
        public Post Get(int postId)
        {
            var post = new Post();

            try
            {
                post = _postsSvc.GetPost(postId) ?? new Post();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return post;
        }

        [HttpGet]
        [Route("api/posts/{postId:int}/related")]
        public RelatedPosts GetRelated(int postId)
        {
            var posts = new RelatedPosts();

            try
            {
                posts = _postsSvc.GetRelatedPosts(postId) ?? new RelatedPosts();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/tag/{tagName}")]
        public List<Post> Get(string tagName)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetPostsByTag(tagName) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/tag/{tagName}/more/{skip}")]
        public List<Post> GetMoreByTag(string tagName, int skip)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetMorePostsByTag(tagName, skip) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/popular")]
        public List<Post> GetPopular()
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetPopularPosts(Convert.ToInt32(_configurationHelper.GetAppSettings("DefaultPostsThreshold"))) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/popular/more/{skip:int?}")]
        public List<Post> GetMorePopular(int skip = 10)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetMorePopularPosts(Convert.ToInt32(_configurationHelper.GetAppSettings("MorePostsTakeValue")), skip) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/recent")]
        public List<Post> GetRecent()
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetRecentPosts(Convert.ToInt32(_configurationHelper.GetAppSettings("DefaultPostsThreshold"))) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/recent/more/{skip:int?}")]
        public List<Post> GetMoreRecent(int skip = 10)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetMoreRecentPosts(Convert.ToInt32(_configurationHelper.GetAppSettings("MorePostsTakeValue")), skip) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/posts")]
        public List<Post> GetUserPosts(int userId)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetPostsByUser(userId) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/posts/more/{skip}")]
        public List<Post> GetMoreUserPosts(int userId, int skip)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetMorePostsByUser(userId, skip) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return posts;
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
    }
}
