using System;
using System.Web.Http;
using Blog.Backend.Api.Rest.Models;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUser _user;

        public UsersController(IUser user)
        {
            _user = user;
        }

        public User Get(int id)
        {
            var user = new User();

            try
            {
                user = _user.Get(id) ?? new User();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return user;
        }

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

        [AcceptVerbs("POST")]
        [ActionName("Login")]
        public User Login([FromBody]Login credentials)
        {
            try
            {
                return _user.Login(credentials.Username, credentials.Password);
            }
            catch
            {
                return null;
            }
        }

        [AcceptVerbs("POST")]
        [ActionName("Logout")]
        public bool Logout([FromBody]string name)
        {
            try
            {
                return _user.Logout(name);
            }
            catch
            {
                return false;
            }
        }
    }
}
