using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;

namespace Blog.Web.Api.Controllers
{
    public class DebugController : ApiController
    {
        [HttpPost, PreventCrossUserManipulation, Authorize]
        [PreventCrossUserManipulation]
        [Route("api/debug")]
        public bool Post([FromBody] Comment comment)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
