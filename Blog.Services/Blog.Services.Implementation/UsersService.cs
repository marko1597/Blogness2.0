using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
{
    public class UsersService : IUser
    {
        public User GetByCredentials(string username, string password)
        {
            return UsersFactory.GetInstance().CreateUsers().GetByCredentials(username, password);
        }

        public User GetByUserName(string username)
        {
            return UsersFactory.GetInstance().CreateUsers().GetByUserName(username);
        }

        public User Get(int userId)
        {
            return UsersFactory.GetInstance().CreateUsers().Get(userId);
        }

        public bool Add(User user)
        {
            return UsersFactory.GetInstance().CreateUsers().Add(user);
        }

        public bool Update(User user)
        {
            return UsersFactory.GetInstance().CreateUsers().Update(user);
        }
    }
}
