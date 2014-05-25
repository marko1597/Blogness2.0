using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class TagsController : ApiController
    {
        private readonly ITag _tag;

        public TagsController(ITag tag)
        {
            _tag = tag;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return tags;
        }
    }
}
