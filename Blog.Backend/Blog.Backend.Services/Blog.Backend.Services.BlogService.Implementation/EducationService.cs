using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class EducationService : IEducation
    {
        public List<Contracts.BlogObjects.Education> GetByUser(int userId)
        {
            return EducationFactory.GetInstance().CreateEducation().GetByUser(userId);
        }

        public Contracts.BlogObjects.Education Add(Contracts.BlogObjects.Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Add(education);
        }

        public Contracts.BlogObjects.Education Update(Contracts.BlogObjects.Education education)
        {
            return EducationFactory.GetInstance().CreateEducation().Update(education);
        }

        public void Delete(Contracts.BlogObjects.Education education)
        {
            EducationFactory.GetInstance().CreateEducation().Delete(education);
        }
    }
}
