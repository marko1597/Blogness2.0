using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class TagController : ApiController
    {
        private readonly IBlogService _service;

        public TagController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetTags")]
        public List<Tag> GetTags(int postId)
        {
            var tags = new List<Tag>();
            try
            {
                tags = _service.GetTags(postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tags;
        }
    }
}
