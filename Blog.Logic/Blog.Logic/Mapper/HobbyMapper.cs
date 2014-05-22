using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class HobbyMapper
    {
        public static Hobby ToDto(DataAccess.Database.Entities.Objects.Hobby hobby)
        {
            return hobby == null ?
                new Hobby() : 
                new Hobby
                {
                    HobbyId = hobby.HobbyId,
                    HobbyName = hobby.HobbyName,
                    UserId = hobby.UserId,
                    CreatedBy = hobby.CreatedBy,
                    CreatedDate = hobby.CreatedDate,
                    ModifiedBy = hobby.ModifiedBy,
                    ModifiedDate = hobby.ModifiedDate
                };
        }

        public static DataAccess.Database.Entities.Objects.Hobby ToEntity(Hobby hobby)
        {
            return hobby == null ?
                new DataAccess.Database.Entities.Objects.Hobby() :
                new DataAccess.Database.Entities.Objects.Hobby
                {
                    HobbyId = hobby.HobbyId,
                    HobbyName = hobby.HobbyName,
                    UserId = hobby.UserId,
                    CreatedBy = hobby.CreatedBy,
                    CreatedDate = hobby.CreatedDate,
                    ModifiedBy = hobby.ModifiedBy,
                    ModifiedDate = hobby.ModifiedDate
                };
        }
    }
}
