using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class Session : ISession
    {
        public Contracts.BlogObjects.Session GetByUser(string username)
        {
            return SessionFactory.GetInstance().CreateSession().GetByUser(username);
        }

        public Contracts.BlogObjects.Session GetByIp(string ipAddress)
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
