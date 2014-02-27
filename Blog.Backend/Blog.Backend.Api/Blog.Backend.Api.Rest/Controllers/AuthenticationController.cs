using System.Web.Http;
using Blog.Backend.Api.Rest.Helper;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IAuthentication _authentication;
        private readonly ISession _session;
        private readonly IUser _user;

        public AuthenticationController(IAuthentication authentication, ISession session, IUser user)
        {
            _authentication = authentication;
            _session = session;
            _user = user;
        }

        [HttpPost]
        [Route("api/authenticate")]
        public bool Authenticate([FromBody] Login login)
        {
            var result = _session.GetByUser(login.Username);
            if (result != null)
            {
                _authentication.SignIn(_user.GetByUserName(null, login.Username));
                return true;
            }
            return false;
        }
    }
}
