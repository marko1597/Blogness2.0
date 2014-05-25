using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class EducationMapper
    {
        public static Education ToDto(Db.Education education)
        {
            return education == null ? null : 
                new Education
                {
                    City = education.City,
                    Country = education.Country,
                    State = education.State,
                    YearAttended = education.YearAttended,
                    YearGraduated = education.YearGraduated,
                    Course = education.Course,
                    EducationType = EducationTypeMapper.ToDto(education.EducationType),
                    EducationId = education.EducationId,
                    UserId = education.UserId,
                    CreatedBy = education.CreatedBy,
                    CreatedDate = education.CreatedDate,
                    ModifiedBy = education.ModifiedBy,
                    ModifiedDate = education.ModifiedDate
                };
        }

        public static Db.Education ToEntity(Education education)
        {
            return education == null ? null : 
                new Db.Education
                {
                    City = education.City,
                    Country = education.Country,
                    Course = education.Course,
                    State = education.State,
                    YearAttended = education.YearAttended,
                    YearGraduated = education.YearGraduated,
                    EducationType = EducationTypeMapper.ToEntity(education.EducationType),
                    EducationTypeId = education.EducationType.EducationTypeId,
                    EducationId = education.EducationId,
                    UserId = education.UserId,
                    CreatedBy = education.CreatedBy,
                    CreatedDate = education.CreatedDate,
                    ModifiedBy = education.ModifiedBy,
                    ModifiedDate = education.ModifiedDate
                };
        }
    }
}
