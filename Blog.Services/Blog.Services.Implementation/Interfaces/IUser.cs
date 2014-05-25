using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IUser
    {
        User GetByCredentials(string username, string password);
        User GetByUserName(string username);
        User Get(int userId);
        User Add(User user);
        User Update(User user);
    }
}
