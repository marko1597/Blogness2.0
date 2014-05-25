using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    public interface ISession
    {
        List<Session> GetAll();
        Session GetByUser(string username);
        Session GetByIp(string ipAddress);
        LoggedUser Login(string userName, string passWord, string ipAddress);
        bool Logout(string userName);
    }
}
