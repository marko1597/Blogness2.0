using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class HobbyMapper
    {
        public static Hobby ToDto(DataAccess.Entities.Objects.Hobby hobby)
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

        public static DataAccess.Entities.Objects.Hobby ToEntity(Hobby hobby)
        {
            return hobby == null ?
                new DataAccess.Entities.Objects.Hobby() :
                new DataAccess.Entities.Objects.Hobby
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
