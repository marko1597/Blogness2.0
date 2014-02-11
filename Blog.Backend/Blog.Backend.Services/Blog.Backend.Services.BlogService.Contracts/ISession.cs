using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System.Collections.Generic;

namespace Blog.Backend.Services.BlogService.Contracts
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
