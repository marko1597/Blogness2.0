using System;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class Users
    {
        private readonly IUserResource _userResource;
        private readonly ISessionResource _sessionResource;

        public Users(IUserResource userResource, ISessionResource sessionResource)
        {
            _userResource = userResource;
            _sessionResource = sessionResource;
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

        public User Get(int userId)
        {
            var user = new User();
            try
            {
                user = _userResource.Get(a => a.UserId == userId, true).FirstOrDefault();
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

        public User Login(string userName, string passWord)
        {
            try
            {
                var user = _userResource.Get(a => a.UserName == userName && a.Password == passWord).FirstOrDefault();
                if (user != null)
                {
                    var session = _sessionResource.Add(user.UserId);
                    CleanupExpiredSessions();

                    if (session != null)
                    {
                        return user;
                    }
                }

                return new User();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public bool Logout(string userName)
        {
            var loggedOut = false;
            try
            {
                var user = _userResource.Get(a => a.UserName == userName).FirstOrDefault();
                if (user != null) loggedOut = _sessionResource.Delete(user.UserId);

                CleanupExpiredSessions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return loggedOut;
        }

        private void CleanupExpiredSessions()
        {
            var oldSessions = _sessionResource.Get(a => a.TimeValidity <= DateTime.UtcNow);
            oldSessions.ForEach(a => _sessionResource.Delete(a.UserId));
        }
    }
}
