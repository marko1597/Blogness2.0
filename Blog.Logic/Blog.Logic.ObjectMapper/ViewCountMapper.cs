using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class ViewCountMapper
    {
        public static ViewCount ToDto(Db.ViewCount viewCount)
        {
            return viewCount == null ? null :
                new ViewCount
                {
                    PostId = viewCount.PostId,
                    Id = viewCount.Id,
                    UserId = viewCount.UserId
                };
        }

        public static Db.ViewCount ToEntity(ViewCount viewCount)
        {
            return viewCount == null ? null :
                new Db.ViewCount
                {
                    PostId = viewCount.PostId,
                    Id = viewCount.Id,
                    UserId = viewCount.UserId
                };
        }
    }
}
