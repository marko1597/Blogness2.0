using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class EducationMock : IEducation
    {
        public EducationMock()
        {
            if (DataStorage.EducationTypes.Count == 0)
            {
                DataStorage.EducationTypes.Add(new EducationType
                {
                    EducationTypeId = 1,
                    EducationTypeName = "Grade School"
                });

                DataStorage.EducationTypes.Add(new EducationType
                {
                    EducationTypeId = 2,
                    EducationTypeName = "High School"
                });

                DataStorage.EducationTypes.Add(new EducationType
                {
                    EducationTypeId = 3,
                    EducationTypeName = "College Education"
                });

                DataStorage.EducationTypes.Add(new EducationType
                {
                    EducationTypeId = 4,
                    EducationTypeName = "Post Graduate"
                });
            }

            if (DataStorage.Educations.Count == 0)
            {
                var educationId = 1;

                foreach (var u in DataStorage.Users)
                {
                    DataStorage.Educations.Add(new Education
                    {
                        EducationId = educationId,
                        UserId = u.UserId,
                        SchoolName = "Grade School",
                        EducationType = DataStorage.EducationTypes.Find(a => a.EducationTypeId == 1),
                        City = "City",
                        State = "State",
                        Country = "Country",
                        YearAttended = DateTime.UtcNow.AddYears(-20),
                        YearGraduated = DateTime.UtcNow.AddYears(-14),
                        Course = string.Empty,
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow
                    });
                    educationId++;

                    DataStorage.Educations.Add(new Education
                    {
                        EducationId = educationId,
                        UserId = u.UserId,
                        SchoolName = "High School",
                        EducationType = DataStorage.EducationTypes.Find(a => a.EducationTypeId == 2),
                        City = "City",
                        State = "State",
                        Country = "Country",
                        YearAttended = DateTime.UtcNow.AddYears(-14),
                        YearGraduated = DateTime.UtcNow.AddYears(-8),
                        Course = string.Empty,
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow
                    });
                    educationId++;

                    DataStorage.Educations.Add(new Education
                    {
                        EducationId = educationId,
                        UserId = u.UserId,
                        SchoolName = "College Education",
                        EducationType = DataStorage.EducationTypes.Find(a => a.EducationTypeId == 3),
                        City = "City",
                        State = "State",
                        Country = "Country",
                        YearAttended = DateTime.UtcNow.AddYears(-8),
                        YearGraduated = DateTime.UtcNow.AddYears(-4),
                        Course = "BS Computer Science",
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow
                    });
                    educationId++;
                }
            }
        }

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
