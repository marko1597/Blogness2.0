using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class UsersService : IUser
    {
        public User GetByCredentials(string username, string password)
        {
            return UsersFactory.GetInstance().CreateLogic().GetByCredentials(username, password);
        }

        public User GetByUserName(string username)
        {
            return UsersFactory.GetInstance().CreateLogic().GetByUserName(username);
        }

        public User Get(int userId)
        {
            return UsersFactory.GetInstance().CreateLogic().Get(userId);
        }

        public User Add(User user)
        {
            return UsersFactory.GetInstance().CreateLogic().Add(user);
        }

        public User Update(User user)
        {
            return UsersFactory.GetInstance().CreateLogic().Update(user);
        }
    }
}
