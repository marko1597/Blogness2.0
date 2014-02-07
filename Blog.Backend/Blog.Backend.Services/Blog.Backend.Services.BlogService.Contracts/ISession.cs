using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface ISession
    {
        Session GetByUser(string username);
        LoggedUser Login(string userName, string passWord);
        bool Logout(string userName);
    }
}
