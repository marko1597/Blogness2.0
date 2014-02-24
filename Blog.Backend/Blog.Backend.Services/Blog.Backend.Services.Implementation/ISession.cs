using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.ViewModels;

namespace Blog.Backend.Services.Implementation
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
