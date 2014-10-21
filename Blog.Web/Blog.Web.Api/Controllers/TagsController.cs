using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class TagsController : ApiController
    {
        private readonly ITagsResource _tag;
        private readonly IErrorSignaler _errorSignaler;

        public TagsController(ITagsResource tag, IErrorSignaler errorSignaler)
        {
            _tag = tag;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/tags/{tagName}")]
        public IHttpActionResult Get(string tagName)
        {
            try
            {
                var tags = _tag.GetByName(tagName) ?? new List<Tag>();
                return Ok(tags);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }
    }
}
