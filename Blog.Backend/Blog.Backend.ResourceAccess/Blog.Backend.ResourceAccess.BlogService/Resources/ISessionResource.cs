using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface ISessionResource
    {
        List<Session> Get(Func<Session, bool> expression);
        Session Add(int userId);
        bool Delete(int userId);
    }
}
