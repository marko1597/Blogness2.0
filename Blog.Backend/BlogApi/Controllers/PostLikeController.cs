using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class PostLikeController : ApiController
    {
        private readonly IBlogService _service;

        public PostLikeController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetLikes")]
        public List<PostLike> GetLikes(int postId)
        {
            var postLikes = new List<PostLike>();
            try
            {
                postLikes = _service.GetPostLikes(postId) ?? new List<PostLike>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return postLikes;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("LikePost")]
        public void LikePost(PostLike postLike)
        {
            try
            {
                _service.AddPostLike(postLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
