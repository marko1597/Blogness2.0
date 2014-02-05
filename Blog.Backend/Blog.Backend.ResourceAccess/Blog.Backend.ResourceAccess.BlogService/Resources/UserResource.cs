using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class UserResource : IUserResource
    {
        public List<User> Get(Func<User, bool> expression)
        {
            return new DbGet().Users(expression);
        }

        public List<User> Get(Func<User, bool> expression, bool isComplete)
        {
            return new DbGet().Users(expression, isComplete);
        }

        public User Add(User user)
        {
            return new DbAdd().User(user);
        }

        public User Update(User user)
        {
            return new DbUpdate().User(user);
        }

        public bool Delete(User user)
        {
            return new DbDelete().User(user);
        }
    }
}
