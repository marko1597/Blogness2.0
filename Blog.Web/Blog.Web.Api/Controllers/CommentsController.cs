using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentsResource _service;
        private readonly IErrorSignaler _errorSignaler;

        public CommentsController(ICommentsResource service, IErrorSignaler errorSignaler)
        {
            _service = service;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/posts/{postId}/comments")]
        public IHttpActionResult GetByPost(int postId)
        {
            try
            {
                var comments = _service.GetByPostId(postId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/user/{userId}/comments")]
        public IHttpActionResult GetByUser(int userId)
        {
            try
            {
                var comments = _service.GetByUser(userId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/comments/{commentId}/replies")]
        public IHttpActionResult Replies(int commentId)
        {
            try
            {
                var comments = _service.GetReplies(commentId);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/comments")]
        public IHttpActionResult Post([FromBody]Comment comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                _service.Add(comment);
                return Ok();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpDelete, Authorize]
        [Route("api/comments/{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }
    }
}
