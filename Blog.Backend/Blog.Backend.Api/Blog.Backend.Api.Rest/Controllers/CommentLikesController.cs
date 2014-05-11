using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class CommentLikesController : ApiController
    {
        private readonly ICommentLikes _service;

        public CommentLikesController(ICommentLikes service)
        {
            _service = service;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
            }
            return commentLikes;
        }

        [HttpPost]
        [Route("api/comments/likes")]
        public void Post([FromBody]CommentLike commentLike)
        {
            try
            {
                _service.Add(commentLike);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
            }
        }
    }
}
