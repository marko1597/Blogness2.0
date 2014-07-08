using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class EducationResource : BaseResource, IEducationResource
    {
        public List<Education> GetByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<IEducationService>("EducationService"))
            {
                return svc.Proxy.GetByUser(userId);
            }
        }

        public Education Add(Education education)
        {
            using (var svc = new ServiceProxyHelper<IEducationService>("EducationService"))
            {
                return svc.Proxy.Add(education);
            }
        }

        public Education Update(Education education)
        {
            using (var svc = new ServiceProxyHelper<IEducationService>("EducationService"))
            {
                return svc.Proxy.Update(education);
            }
        }

        public bool Delete(int educationId)
        {
            using (var svc = new ServiceProxyHelper<IEducationService>("EducationService"))
            {
                return svc.Proxy.Delete(educationId);
            }
        }
    }
}
