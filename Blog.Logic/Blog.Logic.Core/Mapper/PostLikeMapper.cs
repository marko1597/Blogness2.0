using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class PostLikeMapper
    {
        public static PostLike ToDto(DataAccess.Database.Entities.Objects.PostLike postLike)
        {
            return postLike == null ?
                new PostLike() :
                new PostLike
                {
                    PostId = postLike.PostId,
                    PostLikeId = postLike.PostLikeId,
                    UserId = postLike.UserId,
                    CreatedBy = postLike.CreatedBy,
                    CreatedDate = postLike.CreatedDate,
                    ModifiedBy = postLike.ModifiedBy,
                    ModifiedDate = postLike.ModifiedDate
                };
        }

        public static DataAccess.Database.Entities.Objects.PostLike ToEntity(PostLike postLike)
        {
            return postLike == null ?
                new DataAccess.Database.Entities.Objects.PostLike() :
                new DataAccess.Database.Entities.Objects.PostLike
                {
                    PostId = postLike.PostId,
                    PostLikeId = postLike.PostLikeId,
                    UserId = postLike.UserId,
                    CreatedBy = postLike.CreatedBy,
                    CreatedDate = postLike.CreatedDate,
                    ModifiedBy = postLike.ModifiedBy,
                    ModifiedDate = postLike.ModifiedDate
                };
        }
    }
}
