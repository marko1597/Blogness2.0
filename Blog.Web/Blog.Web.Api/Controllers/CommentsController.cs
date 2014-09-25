using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Wcf.Interfaces;

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
        public List<Comment> GetByPost(int postId)
        {
            var comments = new List<Comment>();

            try
            {
                comments = _service.GetByPostId(postId) ?? new List<Comment>();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return comments;
        }

        [HttpGet]
        [Route("api/user/{userId}/comments")]
        public List<Comment> GetByUser(int userId)
        {
            var comments = new List<Comment>();

            try
            {
                comments = _service.GetByUser(userId) ?? new List<Comment>();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return comments;
        }

        [HttpGet]
        [Route("api/comments/{commentId}/replies")]
        public List<Comment> Replies(int commentId)
        {
            var comments = new List<Comment>();

            try
            {
                comments = _service.GetReplies(commentId) ?? new List<Comment>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return comments;
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
                return Ok();
            }
        }

        [HttpDelete, Authorize]
        [Route("api/comments/{id:int}")]
        public void Delete(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
