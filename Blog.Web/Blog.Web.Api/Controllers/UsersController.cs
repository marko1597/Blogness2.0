using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUsersResource _user;
        private readonly IErrorSignaler _errorSignaler;

        public UsersController(IUsersResource user, IErrorSignaler errorSignaler)
        {
            _user = user;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("api/users/{userId:int}")]
        public User Get(int userId)
        {
            var user = new User();

            try
            {
                user = _user.Get(userId) ?? new User();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return user;
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("api/users/{name}")]
        public User Get(string name)
        {
            var user = new User();

            try
            {
                user = _user.GetByUserName(name) ?? new User();

            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }

            return user;
        }

        [HttpPost]
        [Route("api/users")]
        public void Post([FromBody] User user)
        {
            try
            {
                _user.Add(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        [HttpPut]
        [Route("api/users")]
        public void Put([FromBody] User user)
        {
            try
            {
                _user.Update(user);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
