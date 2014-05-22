using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class EducationMock : IEducation
    {
        public List<Education> GetByUser(int userId)
        {
            var educ = DataStorage.Educations.FindAll(a => a.UserId == userId);
            return educ;
        }

        public bool Add(Education education)
        {
            var id = DataStorage.Educations.Select(a => a.EducationId).Max();
            education.EducationId = id + 1;
            DataStorage.Educations.Add(education);

            return true;
        }

        public bool Update(Education education)
        {
            var tEduc = DataStorage.Educations.FirstOrDefault(a => a.EducationId == education.EducationId);
            DataStorage.Educations.Remove(tEduc);
            DataStorage.Educations.Add(education);

            return true;
        }

        public bool Delete(int educationId)
        {
            var tEduc = DataStorage.Educations.FirstOrDefault(a => a.EducationId == educationId);
            DataStorage.Educations.Remove(tEduc);

            return true;
        }
    }
}
