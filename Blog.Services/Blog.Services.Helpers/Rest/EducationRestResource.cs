using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class EducationRestResource : IEducationRestResource
    {
        public List<Education> GetByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Education Add(Education education, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public Education Update(Education education, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int educationId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
