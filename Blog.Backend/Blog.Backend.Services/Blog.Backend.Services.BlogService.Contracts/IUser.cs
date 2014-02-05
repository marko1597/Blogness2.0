using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IUser
    {
        User GetByUserName(int? userId, string userName);
        User Get(int userId);
        User Add(User user);
        User Update(User user);
        User Login(string userName, string passWord);
        bool Logout(string userName);
    }
}
