using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IEducationResource : IBaseResource
    {
        List<Education> GetByUser(int userId);
        Education Add(Education education);
        Education Update(Education education);
        bool Delete(int educationId);
    }
}
