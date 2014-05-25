using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class HobbyMapper
    {
        public static Hobby ToDto(Db.Hobby hobby)
        {
            return hobby == null ? null : 
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

        public static Db.Hobby ToEntity(Hobby hobby)
        {
            return hobby == null ? null : 
                new Db.Hobby
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
