using System;
using System.Web.Http;
using Blog.Common.Web.Attributes;

namespace Blog.Web.Api.Controllers
{
    public class DebugController : ApiController
    {
        [HttpGet]
        [Route("api/debug/sendmessage/{message}")]
        [BlogApiAuthorization]
        public string Get(string message)
        {
            try
            {
                return message;
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return string.Empty;
            }
        }
    }
}
