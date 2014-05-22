using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class EducationMapper
    {
        public static Education ToDto(DataAccess.Database.Entities.Objects.Education education)
        {
            return education == null ?
                new Education() : 
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

        public static DataAccess.Database.Entities.Objects.Education ToEntity(Education education)
        {
            return education == null ?
                new DataAccess.Database.Entities.Objects.Education() : 
                new DataAccess.Database.Entities.Objects.Education
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
