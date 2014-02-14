using System;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        [HttpGet]
        [Route("api/user/{userId:int}")]
        public User Get(int userId)
        {
            var user = new User();

            try
            {
                user = _user.Get(userId) ?? new User();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }

        [HttpGet]
        [Route("api/user/{name}")]
        public User Get(string name)
        {
            var user = new User();

            try
            {
                user = _user.GetByUserName(null, name) ?? new User();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }

        public void Post([FromBody] User user)
        {
            try
            {
                _user.Add(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Put([FromBody] User user)
        {
            try
            {
                _user.Update(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
