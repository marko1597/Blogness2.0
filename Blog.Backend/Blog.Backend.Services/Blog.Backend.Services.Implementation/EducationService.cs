using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
{
    public class EducationService : IEducation
    {
        public List<Education> GetByUser(int userId)
        {
            return EducationFactory.GetInstance().CreateEducation().GetByUser(userId);
        }

        public bool Add(Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Add(education);
        }

        public bool Update(Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Update(education);
        }

        public bool Delete(Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Delete(education);
        }
    }
}
