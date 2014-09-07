using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class EducationMapper
    {
        public static Education ToDto(Db.Education education)
        {
            if (education == null) return null;

            return new Education
                {
                    City = education.City,
                    Country = education.Country,
                    State = education.State,
                    YearAttended = education.YearAttended,
                    YearGraduated = education.YearGraduated,
                    Course = education.Course,
                    SchoolName = education.SchoolName,
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
            if (education == null) return null;
            if (education.EducationType == null) return null;

            return new Db.Education
                {
                    City = education.City,
                    Country = education.Country,
                    Course = education.Course,
                    SchoolName = education.SchoolName,
                    State = education.State,
                    YearAttended = education.YearAttended,
                    YearGraduated = education.YearGraduated,
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
