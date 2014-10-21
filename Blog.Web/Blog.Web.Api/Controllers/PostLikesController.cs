using System;
using System.Net;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class PostLikesController : ApiController
    {
        private readonly IPostLikesResource _service;
        private readonly IUsersResource _user;
        private readonly IErrorSignaler _errorSignaler;

        public PostLikesController(IPostLikesResource service, IUsersResource user, IErrorSignaler errorSignaler)
        {
            _service = service;
            _user = user;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/posts/{postId}/likes")]
        public IHttpActionResult Get(int postId)
        {
            try
            {
                var postLikes = _service.Get(postId);
                return Ok(postLikes);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
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
