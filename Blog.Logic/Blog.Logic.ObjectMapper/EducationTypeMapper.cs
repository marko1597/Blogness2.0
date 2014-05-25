using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class EducationTypeMapper
    {
        public static EducationType ToDto(Db.EducationType educationType)
        {
            return educationType == null ? null : 
                new EducationType
                {
                    EducationTypeId = educationType.EducationTypeId,
                    EducationTypeName = educationType.EducationTypeName
                };
        }

        public static Db.EducationType ToEntity(EducationType educationType)
        {
            return educationType == null ? null : 
                new Db.EducationType
                {
                    EducationTypeId = educationType.EducationTypeId,
                    EducationTypeName = educationType.EducationTypeName
                };
        }
    }
}
