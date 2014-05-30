using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    public class SessionLogicTest
    {
        private Mock<ISessionRepository> _sessionRepository;
        private Mock<IUserRepository> _userRepository;
        private SessionLogic _sessionLogic;
        private List<Session> _sessions;
        private List<User> _users;

        [SetUp]
        public void TestInit()
        {
            #region Sessions

            _sessions = new List<Session>
                    {
                        new Session
                        {
                            SessionId = 1,
                            IpAddress = "::1",
                            TimeValidity = DateTime.Now.AddHours(1),
                            Token = Guid.NewGuid().ToString(),
                            UserId = 1
                        },
                        new Session
                        {
                            SessionId = 2,
                            IpAddress = "::2",
                            TimeValidity = DateTime.Now.AddHours(1),
                            Token = Guid.NewGuid().ToString(),
                            UserId = 2
                        }
                    };

            #endregion

            #region Users

            _users = new List<User>
                     {
                         new User
                         {
                             UserId = 1,
                             UserName = "foo"
                         },
                         new User
                         {
                             UserId = 2,
                             UserName = "bar"
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetAllSessions()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true))
                .Returns(_sessions);

            _userRepository = new Mock<IUserRepository>();

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var sessions = _sessionLogic.GetAll();

            Assert.AreEqual(2, sessions.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetAllSessionFails()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true))
                .Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);
            
            Assert.Throws<BlogException>(() => _sessionLogic.GetAll());
        }

        [Test]
        public void ShouldGetByUser()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();
            var session = _sessions.Where(a => a.UserId == 1).ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(session);
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.GetByUser("foo");

            Assert.NotNull(result);
            Assert.AreEqual(1, result.UserId);
        }

        [Test]
        public void ShouldErrorWhenGetByUserFoundNoUser()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(new List<User>());

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.GetByUser("foo");

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No user with foo as username found", result.Error.Message);
        }

        [Test]
        public void ShouldErrorWhenGetByUserFoundNoSession()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>())).Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.GetByUser("foo");

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No valid session found for user foo", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenUserLookupOnGetByUserFails()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>())).Throws(new Exception());

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.GetByUser("foo"));
        }

        [Test]
        public void ShouldThrowExceptionWhenSessionLookupOnGetByUserFails()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Throws(new Exception());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>())).Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.GetByUser("foo"));
        }

        [Test]
        public void ShouldGetByIp()
        {
            var session = _sessions.Where(a => a.IpAddress == "::1").ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(session);
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.GetByIp("::1");

            Assert.NotNull(result);
            Assert.AreEqual("::1", result.IpAddress);
        }

        [Test]
        public void ShouldErrorWhenGetByIpFoundNoSession()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.GetByIp("::1");

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("No session with ::1 IP address found", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetByIpFoundFails()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Throws(new Exception());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.GetByIp("::1"));
        }

        [Test]
        public void ShouldLogin()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();
            var session = new Session
                          {
                              IpAddress = "::1",
                              Token = Guid.NewGuid().ToString(),
                              TimeValidity = DateTime.Now.AddHours(3),
                              UserId = 1
                          };
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Add(It.IsAny<Session>())).Returns(session);
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.Login("foo", "bar", "::1");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Session);
            Assert.IsNotNull(result.User);
            Assert.IsNull(result.Error);
        }

        [Test]
        public void ShouldErrorWhenLoginHasInvalidCredentials()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(new List<User>());

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.Login("foo", "bar", "::1");

            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.InvalidCredentials, result.Error.Id);
            Assert.AreEqual("Invalid username/password", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenLoginHasFailedOnUserLookup()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.Login("foo", "bar", "::1"));
        }

        [Test]
        public void ShouldThrowExceptionWhenLoginHasFailedOnAddSession()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));
            _sessionRepository.Setup(a => a.Add(It.IsAny<Session>())).Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.Login("foo", "bar", "::1"));
        }

        [Test]
        public void ShouldLogout()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();
            var session = new List<Session>
            {
                new Session
                {
                    SessionId = 1,
                    IpAddress = "::1",
                    Token = Guid.NewGuid().ToString(),
                    TimeValidity = DateTime.Now.AddHours(3),
                    UserId = 1
                }
            };

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(session);
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.Logout("foo");

            Assert.IsNull(result);
        }

        [Test]
        public void ShouldErrorWhenLogoutFoundNoUser()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(new List<User>());

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.Logout("foo");

            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Id);
            Assert.AreEqual("No user found with username foo", result.Message);
        }

        [Test]
        public void ShouldErrorWhenLogoutFoundNoSession()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));
            _sessionRepository.Setup(a => a.Add(It.IsAny<Session>())).Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            var result = _sessionLogic.Logout("foo");

            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Id);
            Assert.AreEqual("No session found for username foo", result.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenLogoutHasFailedOnUserLookup()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.Logout("foo"));
        }

        [Test]
        public void ShouldThrowExceptionWhenLogoutHasFailedOnSessionLookup()
        {
            var user = _users.Where(a => a.UserName == "foo").ToList();

            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), true)).Throws(new Exception());
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>()));

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(),
                It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(), It.IsAny<string>()))
                .Returns(user);

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.Logout("foo"));
        }

        [Test]
        public void ShouldThrowExceptionWhenCleanUpSessionFails()
        {
            _sessionRepository = new Mock<ISessionRepository>();
            _sessionRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Session, bool>>>(), false)).Returns(new List<Session>());
            _sessionRepository.Setup(a => a.Delete(It.IsAny<Session>())).Throws(new Exception());

            _userRepository = new Mock<IUserRepository>();

            _sessionLogic = new SessionLogic(_sessionRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _sessionLogic.GetByUser("foo"));
        }
    }
}
