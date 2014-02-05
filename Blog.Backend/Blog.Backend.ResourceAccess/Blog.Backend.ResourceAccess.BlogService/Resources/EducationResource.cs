using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class EducationResource : IEducationResource
    {
        public List<Education> Get(Func<Education, bool> expression)
        {
            return new DbGet().Education(expression);
        }

        public Education Add(Education education)
        {
            return new DbAdd().Education(education);
        }

        public Education Update(Education education)
        {
            return new DbUpdate().Education(education);
        }

        public bool Delete(Education education)
        {
            return new DbDelete().Education(education);
        }
    }
}
