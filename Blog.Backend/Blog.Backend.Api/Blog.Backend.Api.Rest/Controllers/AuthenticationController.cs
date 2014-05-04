using System.Web.Http;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Backend.Common.Web.Authentication;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationHelper _authentication;
        private readonly ISession _session;
        private readonly IUser _user;

        public AuthenticationController(IAuthenticationHelper authentication, ISession session, IUser user)
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
                _authentication.SignIn(_user.GetByUserName(login.Username));
                return true;
            }
            return false;
        }
    }
}
