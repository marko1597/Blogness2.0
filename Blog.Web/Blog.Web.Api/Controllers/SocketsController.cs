using System;
using System.Web.Http;
using PostsHubFactory = Blog.Web.Api.Helper.Hub.Factory.PostsHubFactory;

namespace Blog.Web.Api.Controllers
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
