using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IUser
    {
        User GetByCredentials(string username, string password);
        User GetByUserName(string username);
        User Get(int userId);
        bool Add(User user);
        bool Update(User user);
    }
}
