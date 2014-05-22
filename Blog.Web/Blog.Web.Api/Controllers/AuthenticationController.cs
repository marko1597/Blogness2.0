using System.Web.Http;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Services.Implementation;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
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
        public bool Post([FromBody] Login login)
        {
            var result = _session.GetByUser(login.Username);
            if (result != null)
            {
                _authentication.SignIn(_user.GetByUserName(login.Username));
                return true;
            }
            return false;
        }

        [HttpPut]
        [Route("api/authenticate")]
        public bool Put([FromBody] Login login)
        {
            var result = _session.GetByUser(login.Username);
            if (result != null)
            {
                _authentication.SignOut(_user.GetByUserName(login.Username));
                return true;
            }
            return false;
        }
    }
}
