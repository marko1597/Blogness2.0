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
        public List<Tag> Get(string tagName)
        {
            var tags = new List<Tag>();

            try
            {
                tags = _tag.GetByName(tagName) ?? new List<Tag>();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return tags;
        }
    }
}
