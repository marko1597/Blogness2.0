using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class SessionLogic
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        public SessionLogic(ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        public List<Session> GetAll()
        {
            var sessions = new List<Session>();
            try
            {
                var db = _sessionRepository.Find(a => a.SessionId > 0, true).ToList();
                db.ForEach(a => sessions.Add(SessionMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return sessions;
        }

        public Session GetByUser(string username)
        {
            try
            {
                CleanupExpiredSessions();
                var user = _userRepository.Find(a => a.UserName == username, null, string.Empty).FirstOrDefault();

                if (user != null)
                {
                    var session = _sessionRepository.Find(a => a.UserId == user.UserId, true).FirstOrDefault();

                    if (session != null)
                    {
                        return SessionMapper.ToDto(session);
                    }

                    return new Session().GenerateError<Session>((int)Constants.Error.RecordNotFound,
                        string.Format("No valid session found for user {0}", username));
                }

                return new Session().GenerateError<Session>((int)Constants.Error.RecordNotFound,
                        string.Format("No user with {0} as username found", username));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Session GetByIp(string ipAddress)
        {
            try
            {
                CleanupExpiredSessions();

                var session = _sessionRepository.Find(a => a.IpAddress == ipAddress, true).FirstOrDefault();
                if (session != null)
                {
                    return SessionMapper.ToDto(session);
                }

                return new Session().GenerateError<Session>((int)Constants.Error.RecordNotFound,
                       string.Format("No session with {0} IP address found", ipAddress));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            try
            {
                DeleteSessionFromSameIp(ipAddress);

                var user = _userRepository.Find(a => a.UserName == userName && a.Password == passWord, null, string.Empty).FirstOrDefault();
                if (user == null || user.UserId == 0)
                {
                    return new LoggedUser().GenerateError<LoggedUser>(
                        (int)Constants.Error.InvalidCredentials,
                        "Invalid username/password");
                }

                var session = new Session
                {
                    IpAddress = ipAddress,
                    SessionId = 0,
                    TimeValidity = DateTime.Now.AddHours(12),
                    Token = Guid.NewGuid().ToString(),
                    UserId = user.UserId
                };

                var dbSession = _sessionRepository.Add(SessionMapper.ToEntity(session));

                CleanupExpiredSessions();

                return new LoggedUser
                {
                    User = UserMapper.ToDto(user),
                    Session = SessionMapper.ToDto(dbSession)
                };
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Error Logout(string userName)
        {
            try
            {
                var user = _userRepository.Find(a => a.UserName == userName, null, string.Empty).FirstOrDefault();

                if (user == null)
                    return new Error
                    {
                        Id = (int)Constants.Error.RecordNotFound,
                        Message = string.Format("No user found with username {0}", userName)
                    };

                var session = _sessionRepository.Find(a => a.UserId == user.UserId, true).FirstOrDefault();

                if (session == null)
                    return new Error
                    {
                        Id = (int)Constants.Error.RecordNotFound,
                        Message = string.Format("No session found for username {0}", userName)
                    };

                _sessionRepository.Delete(session);
                CleanupExpiredSessions();
                return null;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private void DeleteSessionFromSameIp(string ipAddress)
        {
            try
            {
                var sessions = _sessionRepository.Find(a => a.IpAddress == ipAddress, false).ToList();
                sessions.ForEach(a => _sessionRepository.Delete(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void CleanupExpiredSessions()
        {
            try
            {
                var oldSessions = _sessionRepository.Find(a => a.TimeValidity <= DateTime.Now, false).ToList();
                oldSessions.ForEach(a => _sessionRepository.Delete(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
