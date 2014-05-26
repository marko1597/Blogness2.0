using System;
using System.Web.Http;
using Blog.Common.Web.Extensions.Elmah;
using PostsHubFactory = Blog.Web.Api.Helper.Hub.Factory.PostsHubFactory;

namespace Blog.Web.Api.Controllers
{
    public class SocketsController : ApiController
    {
        private readonly IErrorSignaler _errorSignaler;

        public SocketsController(IErrorSignaler errorSignaler)
        {
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/sockets/message/{message}")]
        public void Get(string message)
        {
            try
            {
                PostsHubFactory.GetInstance().Create().PushTestMessage(message);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
