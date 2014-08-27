using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Web.Api.Helper.Hub;

namespace Blog.Web.Api.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly ICommentsResource _service;
        private readonly IErrorSignaler _errorSignaler;
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public CommentsController(ICommentsResource service, IErrorSignaler errorSignaler, IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _service = service;
            _errorSignaler = errorSignaler;
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
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

        [HttpPost, Authorize]
        [Route("api/comments")]
        public IHttpActionResult Post([FromBody]CommentAdded comment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var validationResult = IsValidComment(comment);
                if (validationResult != null)
                {
                    throw new Exception(validationResult.Message);
                }

                var tComment = _service.Add(comment.Comment);
                var commentAdded = new CommentAdded
                                   {
                                       Comment = tComment,
                                       PostId = comment.PostId
                                   };
                new CommentsHub(_errorSignaler, _httpClientHelper, _configurationHelper)
                    .CommentAddedForPost(commentAdded);

                return Ok();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return Ok();
            }
        }

        [HttpDelete, Authorize]
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

        private Error IsValidComment(CommentAdded commentAdded)
        {
            if (commentAdded == null)
            {
                return new Error{ Id = (int)Constants.Error.ValidationError, Message = "There was no comment to be submitted." };
            }

            if (commentAdded.PostId == null)
            {
                return new Error { Id = (int)Constants.Error.ValidationError, Message = "Comment should be attached to a post." };
            }

            return null;
        }
    }
}
