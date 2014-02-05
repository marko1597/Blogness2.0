using System;
using System.Web.Http;
using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IBlogService _service;

        public UserController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetUser")]
        public User GetUser(int userId, string userName)
        {
            var user = new User();
            try
            {
                user = _service.GetUser(userId, userName) ?? new User();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetUserProfile")]
        public User GetUserProfile(int userId)
        {
            var user = new User();
            try
            {
                user = _service.GetUserProfile(userId) ?? new User();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetUserEducation")]
        public List<Education> GetUserEducation(int userId)
        {
            var education = new List<Education>();
            try
            {
                education = _service.GetUserEducation(userId) ?? new List<Education>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return education;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetUserHobbies")]
        public List<Hobby> GetUserHobbies(int userId)
        {
            var hobbies = new List<Hobby>();
            try
            {
                hobbies = _service.GetUserHobbies(userId) ?? new List<Hobby>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return hobbies;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("IsLoggedIn")]
        public Session IsLoggedIn(int userId)
        {
            var session = new Session();
            try
            {
                session = _service.IsLoggedIn(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return session;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Login")]
        public User Login(string username, string password)
        {
            var user = new User();
            try
            {
                user = _service.Login(username, password) ?? new User();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("LogOut")]
        public bool LogOut(string username)
        {
            var hasLoggedOut = false;
            try
            {
                hasLoggedOut = _service.Logout(username);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return hasLoggedOut;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("RegisterUser")]
        public User RegisterUser(string username, string password, string emailAddress, string firstName, string lastName)
        {
            var user = new User();
            try
            {
                user = _service.RegisterUser(username, password, emailAddress, firstName, lastName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }
    }
}
