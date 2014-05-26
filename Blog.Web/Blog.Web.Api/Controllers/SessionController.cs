using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class SessionController : ApiController
    {
        private readonly ISession _session;
        private readonly IErrorSignaler _errorSignaler;

        public SessionController(ISession session, IErrorSignaler errorSignaler)
        {
            _session = session;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/session")]
        public List<Session> Get()
        {
            try
            {
                return _session.GetAll();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
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
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Session
                {
                    Error = new Error
                    {
                        Id = (int)Constants.Error.InternalError,
                        Message = ex.Message,
                        Exception = ex
                    }
                };
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
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Session
                {
                    Error = new Error
                    {
                        Id = (int)Constants.Error.InternalError,
                        Message = ex.Message,
                        Exception = ex
                    }
                };
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
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new LoggedUser
                {
                    Error = new Error
                    {
                        Id = (int) Constants.Error.InternalError,
                        Message = ex.Message,
                        Exception = ex
                    }
                };
            }
        }

        [HttpPut]
        [Route("api/session")]
        public Error Put([FromBody]Login credentials)
        {
            try
            {
                return _session.Logout(credentials.Username);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Error
                {
                    Id = (int)Constants.Error.InternalError,
                    Message = ex.Message,
                    Exception = ex
                };
            }
        }
    }
}
