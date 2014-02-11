using System;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System.Collections.Generic;

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

        public List<Services.BlogService.Contracts.BlogObjects.Session> GetAll()
        {
            try
            {
                var sessions = _sessionResource.Get(a => a.SessionId > 0).ToList();
                return sessions ?? new List<Services.BlogService.Contracts.BlogObjects.Session>();
            }
            catch (Exception)
            {
                return new List<Services.BlogService.Contracts.BlogObjects.Session>();
            }
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

        public Services.BlogService.Contracts.BlogObjects.Session GetByIp(string ipAddress)
        {
            try
            {
                CleanupExpiredSessions();
                var session = _sessionResource.Get(a => a.IpAddress == ipAddress).FirstOrDefault();

                return session ?? new Services.BlogService.Contracts.BlogObjects.Session();
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Session();
            }
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            try
            {
                var user = _userResource.Get(a => a.UserName == userName && a.Password == passWord).FirstOrDefault();
                if (user != null)
                {
                    DeleteSessionFromSameIp(ipAddress);

                    var session = _sessionResource.Add(user.UserId, ipAddress);
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
                var session = _sessionResource.Get(a => a.UserId == user.UserId).FirstOrDefault();

                if (user != null) loggedOut = _sessionResource.Delete(session);

                CleanupExpiredSessions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return loggedOut;
        }

        private void DeleteSessionFromSameIp(string ipAddress)
        {
            try
            {
                var sessions = _sessionResource.Get(a => a.IpAddress == ipAddress);
                sessions.ForEach(a => _sessionResource.Delete(a));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CleanupExpiredSessions()
        {
            var oldSessions = _sessionResource.Get(a => a.TimeValidity <= DateTime.UtcNow);
            oldSessions.ForEach(a => _sessionResource.Delete(a));
        }
    }
}
