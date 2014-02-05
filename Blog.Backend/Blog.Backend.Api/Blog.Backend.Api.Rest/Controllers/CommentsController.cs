using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

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
        public List<Comment> Get(int postId)
        {
            var comments = new List<Comment>();

            try
            {
                comments = _service.GetByPostId(postId) ?? new List<Comment>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        [AcceptVerbs("GET")]
        [Route("api/comment/{commentId}/replies")]
        public List<Comment> Replies(int commentId)
        {
            var comments = new List<Comment>();

            try
            {
                comments = _service.GetReplies(commentId) ?? new List<Comment>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public void Post([FromBody]Comment comment)
        {
            try
            {
                _service.Add(comment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete([FromBody]int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
