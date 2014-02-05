using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class HobbyResource : IHobbyResource
    {
        public List<Hobby> Get(Func<Hobby, bool> expression)
        {
            return new DbGet().Hobbies(expression);
        }

        public Hobby Add(Hobby hobby)
        {
            return new DbAdd().Hobby(hobby);
        }

        public Hobby Update(Hobby hobby)
        {
            return new DbUpdate().Hobby(hobby);
        }

        public bool Delete(Hobby hobby)
        {
            return new DbDelete().Hobby(hobby);
        }
    }
}
