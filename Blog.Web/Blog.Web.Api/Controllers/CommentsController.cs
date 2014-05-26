using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class CommentsController : ApiController
    {
        private readonly IComments _service;
        private readonly IErrorSignaler _errorSignaler;

        public CommentsController(IComments service, IErrorSignaler errorSignaler)
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
        [Route("api/users/{userId}/comments")]
        public List<Comment> GetByComments(int userId)
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

        [HttpPost]
        [Route("api/comments")]
        public void Post([FromBody]Comment comment)
        {
            try
            {
                _service.Add(comment);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        [HttpDelete]
        [Route("api/comments")]
        public void Delete([FromBody]int id)
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
