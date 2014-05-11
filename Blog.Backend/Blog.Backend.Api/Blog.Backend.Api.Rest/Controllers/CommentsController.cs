using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class CommentsController : ApiController
    {
        private readonly IComments _service;

        public CommentsController(IComments service)
        {
            _service = service;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
            }
        }
    }
}
