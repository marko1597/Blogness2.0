using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Api.Rest.Models;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System.Web;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class SessionController : ApiController
    {
        private readonly ISession _session;
        private readonly IUser _user;

        public SessionController(ISession session, IUser user)
        {
            _session = session;
            _user = user;
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

        [HttpDelete]
        [Route("api/session")]
        public bool Delete([FromBody]Login credentials)
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
