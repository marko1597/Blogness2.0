using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
{
    public class SessionService : ISession
    {
        public List<Session> GetAll()
        {
            return SessionFactory.GetInstance().CreateSession().GetAll();
        }

        public Session GetByUser(string username)
        {
            return SessionFactory.GetInstance().CreateSession().GetByUser(username);
        }

        public Session GetByIp(string ipAddress)
        {
            return SessionFactory.GetInstance().CreateSession().GetByIp(ipAddress);
        }

        public LoggedUser Login(string userName, string passWord, string ipAddress)
        {
            return SessionFactory.GetInstance().CreateSession().Login(userName, passWord, ipAddress);
        }

        public bool Logout(string userName)
        {
            return SessionFactory.GetInstance().CreateSession().Logout(userName);
        }
    }
}
