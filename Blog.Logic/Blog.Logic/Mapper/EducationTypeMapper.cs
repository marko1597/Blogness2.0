using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class EducationTypeMapper
    {
        public static EducationType ToDto(DataAccess.Database.Entities.Objects.EducationType educationType)
        {
            return educationType == null ? 
                new EducationType() : 
                new EducationType
                {
                    EducationTypeId = educationType.EducationTypeId,
                    EducationTypeName = educationType.EducationTypeName
                };
        }

        public static DataAccess.Database.Entities.Objects.EducationType ToEntity(EducationType educationType)
        {
            return educationType == null ?
                new DataAccess.Database.Entities.Objects.EducationType() : 
                new DataAccess.Database.Entities.Objects.EducationType
                {
                    EducationTypeId = educationType.EducationTypeId,
                    EducationTypeName = educationType.EducationTypeName
                };
        }
    }
}
