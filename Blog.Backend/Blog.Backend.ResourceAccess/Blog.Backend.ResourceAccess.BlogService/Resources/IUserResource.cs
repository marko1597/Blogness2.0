using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IUserResource
    {
        List<User> Get(Func<User, bool> expression);
        User Add(User user);
        User Update(User user);
        bool Delete(User user);
    }
}
