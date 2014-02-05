using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IHobbyResource
    {
        List<Hobby> Get(Func<Hobby, bool> expression);
        Hobby Add(Hobby hobby);
        Hobby Update(Hobby hobby);
        bool Delete(Hobby hobby);
    }
}
