using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class CommentLikesController : ApiController
    {
        private readonly ICommentLikes _service;

        public CommentLikesController(ICommentLikes service)
        {
            _service = service;
        }

        public List<CommentLike> Get(int id)
        {
            var commentLikes = new List<CommentLike>();

            try
            {
                commentLikes = _service.Get(id) ?? new List<CommentLike>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return commentLikes;
        }

        public void Post([FromBody]CommentLike commentLike)
        {
            try
            {
                _service.Add(commentLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
