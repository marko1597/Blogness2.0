using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface ISession
    {
        Session GetByUser(string username);
        Session GetByIp(string ipAddress);
        LoggedUser Login(string userName, string passWord, string ipAddress);
        bool Logout(string userName);
    }
}
