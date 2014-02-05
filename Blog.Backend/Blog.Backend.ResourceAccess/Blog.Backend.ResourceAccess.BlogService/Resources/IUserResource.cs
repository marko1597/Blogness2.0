using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IUserResource
    {
        List<User> Get(Func<User, bool> expression);
        List<User> Get(Func<User, bool> expression, bool isComplete);
        User Add(User user);
        User Update(User user);
        bool Delete(User user);
    }
}
