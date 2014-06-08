using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class EducationService : IEducation
    {
        public List<Education> GetByUser(int userId)
        {
            return EducationFactory.GetInstance().CreateEducation().GetByUser(userId);
        }

        public Education Add(Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Add(education);
        }

        public Education Update(Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Update(education);
        }

        public bool Delete(int educationId)
        {
            return EducationFactory.GetInstance().CreateEducation().Delete(educationId);
        }
    }
}
