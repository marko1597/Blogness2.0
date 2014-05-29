using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;
using Blog.Web.Api.Helper.Hub;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class PostLikesController : ApiController
    {
        private readonly IPostLikes _service;
        private readonly IUser _user;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public PostLikesController(IPostLikes service, IUser user, IErrorSignaler errorSignaler, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _service = service;
            _user = user;
            _errorSignaler = errorSignaler;
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
        }

        [HttpGet]
        [Route("api/posts/{postId}/likes")]
        public List<PostLike> Get(int postId)
        {
            var postLikes = new List<PostLike>();

            try
            {
                postLikes = _service.Get(postId) ?? new List<PostLike>();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return postLikes;
        }

        [HttpPost]
        [Route("api/posts/likes")]
        public void Post([FromUri]int postId, string username)
        {
            try
            {
                var user = _user.GetByUserName(username);
                var postLike = new PostLike
                               {
                                   PostId = postId,
                                   UserId = user.UserId
                               };
                _service.Add(postLike);
                new PostsHub(_errorSignaler, _httpClientHelper, _configurationHelper).PushPostLikes(new PostLikesUpdate
                {
                    PostId = postId,
                    PostLikes = _service.Get(postId)
                });
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
