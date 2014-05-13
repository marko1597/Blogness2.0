using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.Utils;
using Blog.Backend.Common.Contracts.ViewModels;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class SessionMock : ISession
    {
        public List<Session> GetAll()
        {
            return DataStorage.Sessions;
        }

        public Session GetByUser(string username)
        {
            var user = DataStorage.Users.FirstOrDefault(a => a.UserName == username);
            var session = DataStorage.Sessions.FindAll(a => user != null && a.UserId == user.UserId);

            return session.FirstOrDefault();
        }

        public Session GetByIp(string ipAddress)
        {
            var session = DataStorage.Sessions.FirstOrDefault(a => a.IpAddress == ipAddress);
            return session;
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            DeleteSessionFromSameIp(ipAddress);
            var user = DataStorage.Users.FindAll(a => a.UserName == userName && a.Password == passWord);

            if (user.Count > 0)
            {
                var id = DataStorage.Sessions.Select(a => a.SessionId).Max();
                var session = new Session
                              {
                                  IpAddress = ipAddress,
                                  UserId = user.First().UserId,
                                  SessionId = id + 1,
                                  TimeValidity = DateTime.UtcNow.AddMinutes(20),
                                  Token = Guid.NewGuid().ToString()
                              };

                CleanupExpiredSessions();
                return new LoggedUser
                       {
                           Session = session,
                           User = user.FirstOrDefault()
                       };
            }

            CleanupExpiredSessions();
            return new LoggedUser();
        }

        public bool Logout(string userName)
        {
            var user = DataStorage.Users.FirstOrDefault(a => a.UserName == userName);
            var tSession = DataStorage.Sessions.FirstOrDefault(a => user != null && a.UserId == user.UserId);
            DataStorage.Sessions.Remove(tSession);
            CleanupExpiredSessions();

            return true;
        }


        private void DeleteSessionFromSameIp(string ipAddress)
        {
            try
            {
                var sessions = DataStorage.Sessions.FindAll(a => a.IpAddress == ipAddress);
                sessions.ForEach(a => DataStorage.Sessions.Remove(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private void CleanupExpiredSessions()
        {
            var oldSessions = DataStorage.Sessions.FindAll(a => a.TimeValidity <= DateTime.UtcNow);
            oldSessions.ForEach(a => DataStorage.Sessions.Remove(a));
        }
    }
}
