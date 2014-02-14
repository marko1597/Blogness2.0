using System;
using System.Linq;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System.Collections.Generic;

namespace Blog.Backend.Logic.BlogService
{
    public class SessionLogic
    {
        private readonly ISessionResource _sessionResource;

        public SessionLogic(ISessionResource sessionResource)
        {
            _sessionResource = sessionResource;
        }

        public List<Session> GetAll()
        {
            try
            {
                var sessions = _sessionResource.Get(a => a.SessionId > 0).ToList();
                return sessions;
            }
            catch (Exception)
            {
                return new List<Session>();
            }
        }

        public Session GetByUser(string username)
        {
            try
            {
                CleanupExpiredSessions();
                var user = UsersFactory.GetInstance().CreateUsers().GetByUserName(null, username);
                var session = _sessionResource.Get(a => user != null && a.UserId == user.UserId).FirstOrDefault();

                return session ?? new Session();
            }
            catch (Exception)
            {
                return new Session();
            }
        }

        public Session GetByIp(string ipAddress)
        {
            try
            {
                CleanupExpiredSessions();
                var session = _sessionResource.Get(a => a.IpAddress == ipAddress).FirstOrDefault();

                return session ?? new Session();
            }
            catch (Exception)
            {
                return new Session();
            }
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            try
            {
                var user = UsersFactory.GetInstance().CreateUsers().GetByCredentials(userName, passWord);
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
                var user = UsersFactory.GetInstance().CreateUsers().GetByUserName(null, userName);
                var session = _sessionResource.Get(a => user != null && a.UserId == user.UserId).FirstOrDefault();

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
