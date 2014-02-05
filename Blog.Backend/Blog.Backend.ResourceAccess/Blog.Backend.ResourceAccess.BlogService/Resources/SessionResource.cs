using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class SessionResource : ISessionResource
    {
        public List<Session> Get(Func<Session, bool> expression)
        {
            return new DbGet().Sessions(expression);
        }

        public Session Add(int userId)
        {
            return new DbAdd().Session(userId);
        }

        public bool Delete(int userId)
        {
            return new DbDelete().Session(userId);
        }
    }
}
