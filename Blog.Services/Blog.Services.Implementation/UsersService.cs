using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IUsersLogic _usersLogic;

        public UsersService(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        public User GetByUserName(string username)
        {
            return _usersLogic.GetByUserName(username);
        }

        public User Get(int userId)
        {
            return _usersLogic.Get(userId);
        }

        public User Add(User user)
        {
            return _usersLogic.Add(user);
        }

        public User Update(User user)
        {
            return _usersLogic.Update(user);
        }
    }
}
