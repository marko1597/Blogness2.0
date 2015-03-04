using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IEducationResource : IEducationService
    {
    }

    public interface IEducationRestResource
    {
        List<Education> GetByUser(int userId);
        Education Add(Education education, string authenticationToken);
        Education Update(Education education, string authenticationToken);
        bool Delete(int educationId, string authenticationToken);
    }
}
