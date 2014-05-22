using System.Collections.Generic;
using System.Web.Http;
using System.Web;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Web.Attributes;
using Blog.Services.Implementation;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class SessionController : ApiController
    {
        private readonly ISession _session;
        public SessionController(ISession session)
        {
            _session = session;
        }

        [HttpGet]
        [Route("api/session")]
        public List<Session> Get()
        {
            try
            {
                return _session.GetAll();
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        [Route("api/session/{userName}")]
        public Session GetByUser(string username)
        {
            try
            {
                return _session.GetByUser(username);
            }
            catch
            {
                return null;
            }
        }

        [HttpGet]
        [Route("api/session/ip/{ipAddress}")]
        public Session GetByIp(string ipAddress)
        {
            try
            {
                ipAddress = ipAddress.Replace('x', ':');
                return _session.GetByIp(ipAddress);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("api/session")]
        public LoggedUser Post([FromBody]Login credentials)
        {
            try
            {
                var ip = ((HttpContextBase)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                return _session.Login(credentials.Username, credentials.Password, ip);
            }
            catch
            {
                return null;
            }
        }

        [HttpPut]
        [Route("api/session")]
        public bool Put([FromBody]Login credentials)
        {
            try
            {
                return _session.Logout(credentials.Username);
            }
            catch
            {
                return false;
            }
        }
    }
}
