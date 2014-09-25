using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Web.Api.Helper.Hub;

namespace Blog.Web.Api.Controllers
{
    public class PostLikesController : ApiController
    {
        private readonly IPostLikesResource _service;
        private readonly IUsersResource _user;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public PostLikesController(IPostLikesResource service, IUsersResource user, IErrorSignaler errorSignaler, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
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

        [HttpPost, Authorize]
        [Route("api/posts/likes")]
        public void Post([FromUri]int postId, string username)
        {
            try
            {
                var user = _user.GetByUserName(username);
                var loggedUser = _user.GetByUserName(User.Identity.Name);
                if (loggedUser.Id != user.Id) throw new HttpResponseException(HttpStatusCode.Forbidden);

                var postLike = new PostLike
                               {
                                   PostId = postId,
                                   UserId = user.Id
                               };
                _service.Add(postLike);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
