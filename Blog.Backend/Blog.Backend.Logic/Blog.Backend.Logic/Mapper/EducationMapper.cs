using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class EducationMapper
    {
        public static Education ToDto(DataAccess.Entities.Objects.Education education)
        {
            return education == null ?
                new Education() : 
                new Education
                {
                    City = education.City,
                    Country = education.Country,
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

        public static DataAccess.Entities.Objects.Education ToEntity(Education education)
        {
            return education == null ?
                new DataAccess.Entities.Objects.Education() : 
                new DataAccess.Entities.Objects.Education
                {
                    City = education.City,
                    Country = education.Country,
                    Course = education.Course,
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
