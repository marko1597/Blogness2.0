using System;
using System.Web.Http;
using Blog.Backend.Common.Web.Helper.Hub.Factory;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class SocketsController : ApiController
    {
        [HttpPost]
        [Route("api/sockets/message/{message}")]
        public void Get(string message)
        {
            try
            {
                PostsHubFactory.GetInstance().Create().PushTestMessage(message);
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
