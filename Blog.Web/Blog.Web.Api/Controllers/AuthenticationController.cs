using System;
using System.Web.Http;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Authentication;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationHelper _authentication;
        private readonly ISessionResource _session;
        private readonly IUsersResource _user;
        private readonly IErrorSignaler _errorSignaler;

        public AuthenticationController(IAuthenticationHelper authentication, ISessionResource session, IUsersResource user, IErrorSignaler errorSignaler)
        {
            _authentication = authentication;
            _session = session;
            _user = user;
            _errorSignaler = errorSignaler;
        }

        [HttpPost]
        [Route("api/authenticate")]
        public bool Post([FromBody] Login login)
        {
            try
            {
                var result = _session.GetByUser(login.Username);
                if (result != null && result.Error == null)
                {
                    _authentication.SignIn(_user.GetByUserName(login.Username));
                    _errorSignaler.SignalFromCurrentContext(new Exception(string.Format("User {0} logged in", login.Username)));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return false;
            }
        }

        [HttpPut]
        [Route("api/authenticate")]
        public bool Put([FromBody] Login login)
        {
            try
            {
                _authentication.SignOut(_user.GetByUserName(login.Username));
                _errorSignaler.SignalFromCurrentContext(new Exception(string.Format("User {0} logged off", login.Username)));
                return true;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return false;
            }
        }
    }
}
