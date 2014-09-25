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
    public class CommentLikesController : ApiController
    {
        private readonly IUsersResource _user;
        private readonly ICommentLikesResource _service;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public CommentLikesController(ICommentLikesResource service, IErrorSignaler errorSignaler, IUsersResource user, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _service = service;
            _errorSignaler = errorSignaler;
            _user = user; 
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
        }

        [HttpGet]
        [Route("api/comments/{commentId}/likes")]
        public List<CommentLike> Get(int commentId)
        {
            var commentLikes = new List<CommentLike>();

            try
            {
                commentLikes = _service.Get(commentId) ?? new List<CommentLike>();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return commentLikes;
        }

        [HttpPost, Authorize]
        [Route("api/comments/likes")]
        public void Post([FromUri]int commentId, string username)
        {
            try
            {
                var user = _user.GetByUserName(username);
                var loggedUser = _user.GetByUserName(User.Identity.Name);
                if (loggedUser.Id != user.Id) throw new HttpResponseException(HttpStatusCode.Forbidden);

                var commentLike = new CommentLike
                {
                    CommentId = commentId,
                    UserId = user.Id
                };
                _service.Add(commentLike);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
