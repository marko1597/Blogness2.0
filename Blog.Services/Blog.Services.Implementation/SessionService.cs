using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class SessionService : BaseService, ISessionService
    {
        private readonly ISessionLogic _sessionLogic;

        public SessionService(ISessionLogic sessionLogic)
        {
            _sessionLogic = sessionLogic;
        }

        public List<Session> GetAll()
        {
            return _sessionLogic.GetAll();
        }

        public Session GetByUser(string username)
        {
            return _sessionLogic.GetByUser(username);
        }

        public Session GetByIp(string ipAddress)
        {
            return _sessionLogic.GetByIp(ipAddress);
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            return _sessionLogic.Login(userName, passWord, ipAddress);
        }

        public Error Logout(string userName)
        {
            return _sessionLogic.Logout(userName);
        }
    }
}
