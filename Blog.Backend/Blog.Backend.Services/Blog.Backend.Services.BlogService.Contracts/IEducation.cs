using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IEducation
    {
        List<Education> GetByUser(int userId);
        Education Add(Education education);
        Education Update(Education education);
        void Delete(Education education);
    }
}
