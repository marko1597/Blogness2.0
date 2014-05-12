using System;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Web.Attributes;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    [AllowCrossSiteApi]
    public class UsersController : ApiController
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }

            return user;
        }

        [HttpGet]
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
        }
    }
}
