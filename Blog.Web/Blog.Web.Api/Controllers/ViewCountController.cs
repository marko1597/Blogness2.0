using System;
using System.Web.Http;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Interfaces;

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
        public IHttpActionResult Get(int postId)
        {
            try
            {
                var viewCounts = _service.Get(postId);
                return Ok(viewCounts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }
    }
}
