using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class SessionLogic
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionLogic(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public List<Session> GetAll()
        {
            var sessions = new List<Session>();
            try
            {
                var db = _sessionRepository.Find(a => a.SessionId > 0, true).ToList();
                db.ForEach(a => sessions.Add(SessionMapper.ToDto(a)));
            }
            catch (Exception)
            {
                return new List<Session>();
            }
            return sessions;
        }

        public Session GetByUser(string username)
        {
            try
            {
                CleanupExpiredSessions();
                var user = UsersFactory.GetInstance().CreateUsers().GetByUserName(username);
                var session = new DataAccess.Entities.Objects.Session();

                if (user != null)
                {
                    session = _sessionRepository.Find(a => a.UserId == user.UserId, true).FirstOrDefault();
                }

                return SessionMapper.ToDto(session) ?? new Session();
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
                var session = _sessionRepository.Find(a => a.IpAddress == ipAddress, true).FirstOrDefault();

                return SessionMapper.ToDto(session) ?? new Session();
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
                DeleteSessionFromSameIp(ipAddress);

                var user = UsersFactory.GetInstance().CreateUsers().GetByCredentials(userName, passWord);
                if (user == null || user.UserId == 0) return new LoggedUser { Session = null, User = null };

                var session = new Session
                {
                    IpAddress = ipAddress,
                    SessionId = 0,
                    TimeValidity = DateTime.UtcNow.AddMinutes(20),
                    Token = Guid.NewGuid().ToString(),
                    UserId = user.UserId
                };

                _sessionRepository.Add(SessionMapper.ToEntity(session));

                CleanupExpiredSessions();

                return new LoggedUser
                {
                    User = user,
                    Session = session
                };
            }
            catch (Exception)
            {
                return new LoggedUser
                {
                    User = null,
                    Session = null
                };
            }
        }

        public bool Logout(string userName)
        {
            var loggedOut = false;
            try
            {
                var user = UsersFactory.GetInstance().CreateUsers().GetByUserName(userName);
                var session = _sessionRepository.Find(a => user != null && a.UserId == user.UserId, true).FirstOrDefault();

                if (user != null)
                {
                    _sessionRepository.Delete(session);
                }
                CleanupExpiredSessions();
                loggedOut = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return loggedOut;
        }

        public void DeleteSessionFromSameIp(string ipAddress)
        {
            try
            {
                var sessions = _sessionRepository.Find(a => a.IpAddress == ipAddress, true).ToList();
                sessions.ForEach(a => _sessionRepository.Delete(a));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CleanupExpiredSessions()
        {
            var oldSessions = _sessionRepository.Find(a => a.TimeValidity <= DateTime.UtcNow, true).ToList();
            oldSessions.ForEach(a => _sessionRepository.Delete(a));
        }
    }
}
