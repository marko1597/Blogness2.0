using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class PostLikeMapper
    {
        public static PostLike ToDto(DataAccess.Entities.Objects.PostLike postLike)
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

        public static DataAccess.Entities.Objects.PostLike ToEntity(PostLike postLike)
        {
            return postLike == null ?
                new DataAccess.Entities.Objects.PostLike() :
                new DataAccess.Entities.Objects.PostLike
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
