using System;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Logic.BlogService
{
    public class Session
    {
        private readonly ISessionResource _sessionResource;
        private readonly IUserResource _userResource;

        public Session(ISessionResource sessionResource, IUserResource userResource)
        {
            _sessionResource = sessionResource;
            _userResource = userResource;
        }

        public Services.BlogService.Contracts.BlogObjects.Session GetByUser(string username)
        {
            try
            {
                CleanupExpiredSessions();
                var user = _userResource.Get(a => a.UserName == username).FirstOrDefault();
                var session = _sessionResource.Get(a => a.UserId == user.UserId).FirstOrDefault();

                return session ?? new Services.BlogService.Contracts.BlogObjects.Session();
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Session();
            }
        }

        public LoggedUser Login(string userName, string passWord)
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
                        return new LoggedUser
                        {
                            User = user,
                            Session = session
                        };
                    }
                }

                return new LoggedUser();
            }
            catch (Exception)
            {
                return new LoggedUser();
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
