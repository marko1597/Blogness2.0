using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface ISessionResource : IBaseResource
    {
        List<Session> GetAll();
        Session GetByUser(string username);
        Session GetByIp(string ipAddress);
        LoggedUser Login(string userName, string passWord, string ipAddress);
        Error Logout(string userName);
    }
}
