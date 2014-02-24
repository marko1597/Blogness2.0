using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class EducationTypeMapper
    {
        public static EducationType ToDto(DataAccess.Entities.Objects.EducationType educationType)
        {
            return educationType == null ? 
                new EducationType() : 
                new EducationType
                {
                    EducationTypeId = educationType.EducationTypeId,
                    EducationTypeName = educationType.EducationTypeName
                };
        }

        public static DataAccess.Entities.Objects.EducationType ToEntity(EducationType educationType)
        {
            return educationType == null ?
                new DataAccess.Entities.Objects.EducationType() : 
                new DataAccess.Entities.Objects.EducationType
                {
                    EducationTypeId = educationType.EducationTypeId,
                    EducationTypeName = educationType.EducationTypeName
                };
        }
    }
}
