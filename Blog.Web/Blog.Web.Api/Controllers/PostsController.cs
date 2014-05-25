using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Services.Implementation.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class PostsController : ApiController
    {
        private readonly IPosts _postsSvc;
        private readonly IPostsPage _postsPageSvc;

        public PostsController(IPosts postsSvc, IPostsPage postsPageSvc)
        {
            _postsSvc = postsSvc;
            _postsPageSvc = postsPageSvc;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return post;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                posts = _postsPageSvc.GetPopularPosts(
                    Convert.ToInt32(ConfigurationManager.AppSettings.Get("DefaultPostsThreshold"))) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                posts = _postsPageSvc.GetRecentPosts(
                    Convert.ToInt32(ConfigurationManager.AppSettings.Get("DefaultPostsThreshold"))) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/posts/more/{skip:int?}")]
        public List<Post> GetMore(int skip = 10)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsPageSvc.GetMorePosts(
                    Convert.ToInt32(ConfigurationManager.AppSettings.Get("MorePostsTakeValue")), skip) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return posts;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/posts")]
        public UserPosts GetUserPosts(int userId)
        {
            var posts = new UserPosts();

            try
            {
                posts = _postsPageSvc.GetUserPosts(userId) ?? new UserPosts();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return posts;
        }

        [HttpPost]
        [BlogApiAuthorization]
        [Route("api/posts")]
        public Post Post([FromBody]Post post)
        {
            try
            {
                return _postsSvc.AddPost(post);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return new Post
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = ex.Message
                    }
                };
            }
        }

        [HttpPut]
        [BlogApiAuthorization]
        [Route("api/posts")]
        public Post Put([FromBody]Post post)
        {
            try
            {
                var isAllowed = AuthenticationApiFactory.GetInstance()
                    .Create()
                    .IsUserAllowedAccess(User.Identity.GetUserName(), post.PostId);

                if (!isAllowed)
                {
                    return new Post
                    {
                        Error = new Error
                        {
                            Id = (int)Common.Utils.Constants.Error.RequestNotAllowed,
                            Message = "Request not allowed. You cannot edit someone else's post."
                        }
                    };
                }

                return _postsSvc.UpdatePost(post);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return new Post
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = ex.Message
                    }
                };
            }
        }

        [HttpDelete]
        [BlogApiAuthorization]
        [Route("api/posts")]
        public void Delete([FromBody]int id)
        {
            try
            {
                _postsSvc.DeletePost(id);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
