using System;
using System.Net;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class CommentLikesController : ApiController
    {
        private readonly IUsersResource _user;
        private readonly ICommentLikesResource _service;
        private readonly IErrorSignaler _errorSignaler;

        public CommentLikesController(ICommentLikesResource service, IErrorSignaler errorSignaler, IUsersResource user)
        {
            _service = service;
            _errorSignaler = errorSignaler;
            _user = user;
        }

        [HttpGet]
        [Route("api/comments/{commentId}/likes")]
        public IHttpActionResult Get(int commentId)
        {
            try
            {
                var commentLikes = _service.Get(commentId);
                return Ok(commentLikes);

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
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
