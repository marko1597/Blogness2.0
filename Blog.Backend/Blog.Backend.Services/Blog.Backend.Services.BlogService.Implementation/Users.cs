using System;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class Users : IUser
    {
        public User GetByUserName(int? userId, string userName)
        {
            return UsersFactory.GetInstance().CreateUsers().GetByUserName(userId, userName);
        }

        public User Get(int userId)
        {
            return UsersFactory.GetInstance().CreateUsers().Get(userId);
        }

        public User Add(User user)
        {
            return UsersFactory.GetInstance().CreateUsers().Add(user);
        }

        public User Update(User user)
        {
            return UsersFactory.GetInstance().CreateUsers().Update(user);
        }
    }
}
