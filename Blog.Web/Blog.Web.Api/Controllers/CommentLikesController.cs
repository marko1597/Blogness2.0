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
    public class CommentLikesController : ApiController
    {
        private readonly IUser _user;
        private readonly ICommentLikes _service;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public CommentLikesController(ICommentLikes service, IErrorSignaler errorSignaler, IUser user, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
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

        [HttpPost]
        [Route("api/comments/likes")]
        public void Post([FromUri]int commentId, string username)
        {
            try
            {
                var user = _user.GetByUserName(username);
                var commentLike = new CommentLike
                {
                    CommentId = commentId,
                    UserId = user.UserId
                };
                _service.Add(commentLike);
                new CommentsHub(_errorSignaler, _httpClientHelper, _configurationHelper).CommentsLikeUpdate(new CommentLikesUpdate
                {
                    CommentId = commentId,
                    CommentLikes = _service.Get(commentId)
                });
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
