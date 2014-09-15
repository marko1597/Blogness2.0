using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class EducationService : BaseService, IEducationService
    {
        private readonly IEducationLogic _educationLogic;

        public EducationService(IEducationLogic educationLogic)
        {
            _educationLogic = educationLogic;
        }

        public List<Education> GetByUser(int userId)
        {
            return _educationLogic.GetByUser(userId);
        }

        public Education Add(Education education)
        {
            return _educationLogic.Add(education);
        }

        public Education Update(Education education)
        {
            return _educationLogic.Update(education);
        }

        public bool Delete(int educationId)
        {
            return _educationLogic.Delete(educationId);
        }
    }
}
