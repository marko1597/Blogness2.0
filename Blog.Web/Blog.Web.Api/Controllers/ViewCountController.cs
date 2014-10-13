using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class ViewCountController : ApiController
    {
        private readonly IViewCountResource _service;
        private readonly IErrorSignaler _errorSignaler;
        public ViewCountController(IViewCountResource service, IErrorSignaler errorSignaler)
        {
            _service = service;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/posts/{postId}/viewcount")]
        public List<ViewCount> Get(int postId)
        {
            var viewCounts = new List<ViewCount>();

            try
            {
                viewCounts = _service.Get(postId) ?? new List<ViewCount>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return viewCounts;
        }
    }
}
