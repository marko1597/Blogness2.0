using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class TagsController : ApiController
    {
        private readonly ITag _tag;
        private readonly IErrorSignaler _errorSignaler;

        public TagsController(ITag tag, IErrorSignaler errorSignaler)
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
