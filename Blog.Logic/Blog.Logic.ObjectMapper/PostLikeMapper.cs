using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class PostLikeMapper
    {
        public static PostLike ToDto(Db.PostLike postLike)
        {
            return postLike == null ? null :
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

        public static Db.PostLike ToEntity(PostLike postLike)
        {
            return postLike == null ? null :
                new Db.PostLike
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
