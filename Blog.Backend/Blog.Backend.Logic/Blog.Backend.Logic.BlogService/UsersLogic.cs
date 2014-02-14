using System;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class UsersLogic
    {
        private readonly IUserResource _userResource;

        public UsersLogic(IUserResource userResource)
        {
            _userResource = userResource;
        }

        public User GetByUserName(int? userId, string userName)
        {
            var user = new User();
            try
            {
                user = string.IsNullOrEmpty(userName) ? _userResource.Get(a => a.UserId == userId).FirstOrDefault() : _userResource.Get(a => a.UserName == userName).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public User GetByCredentials(string username, string password)
        {
            var user = new User();
            try
            {
                user = _userResource.Get(a => a.UserName == username && a.Password == password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public User Get(int userId)
        {
            var user = new User();
            try
            {
                user = _userResource.Get(a => a.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return user;
        }

        public User Add(User user)
        {
            try
            {
                return _userResource.Add(user);
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public User Update(User user)
        {
            try
            {
                return _userResource.Update(user);
            }
            catch (Exception)
            {
                return new User();
            }
        }
    }
}
