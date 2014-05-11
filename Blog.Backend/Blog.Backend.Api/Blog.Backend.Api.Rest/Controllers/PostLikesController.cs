using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class PostLikesController : ApiController
    {
        private readonly IPostLikes _service;

        public PostLikesController(IPostLikes service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("api/posts/{postId}/likes")]
        public List<PostLike> Get(int postId)
        {
            var postLikes = new List<PostLike>();

            try
            {
                postLikes = _service.Get(postId) ?? new List<PostLike>();

            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
            }
            return postLikes;
        }

        [HttpPost]
        [Route("api/posts/likes")]
        public void Post([FromBody]PostLike postLike)
        {
            try
            {
                _service.Add(postLike);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
            }
        }
    }
}
