using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IEducationResource
    {
        List<Education> Get(Func<Education, bool> expression);
        Education Add(Education education);
        Education Update(Education education);
        bool Delete(Education education);
    }
}
