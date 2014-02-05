using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class PostContentController : ApiController
    {
        private readonly IBlogService _service;

        public PostContentController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetList")]
        public List<PostContent> GetList(int postId)
        {
            var postImages = new List<PostContent>();
            try
            {
                postImages = _service.GetPostContents(postId) ?? new List<PostContent>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return postImages;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Get")]
        public PostContent Get(int postContentId)
        {
            try
            {
                return _service.GetPostContent(postContentId) ?? new PostContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Add")]
        public bool Add(PostContent postContent)
        {
            try
            {
                _service.AddPostContent(postContent);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Delete")]
        public bool Delete(int postContentId)
        {
            try
            {
                _service.DeletePostContent(postContentId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
