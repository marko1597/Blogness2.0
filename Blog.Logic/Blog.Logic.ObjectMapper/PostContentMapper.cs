using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class PostContentMapper
    {
        public static PostContent ToDto(Db.PostContent postContent)
        {
            return postContent == null ? null : 
                new PostContent
                {
                    Id = postContent.PostContentId,
                    PostId = postContent.PostId,
                    Media = postContent.Media == null ? null : MediaMapper.ToDto(postContent.Media),
                    PostContentText = postContent.PostContentText,
                    PostContentTitle = postContent.PostContentTitle,
                    CreatedBy = postContent.CreatedBy,
                    CreatedDate = postContent.CreatedDate,
                    ModifiedBy = postContent.ModifiedBy,
                    ModifiedDate = postContent.ModifiedDate
                };
        }

        public static Db.PostContent ToEntity(PostContent postContent)
        {
            return postContent == null ? null : 
                new Db.PostContent
                {
                    PostContentId = postContent.Id,
                    PostId = postContent.PostId,
                    Media = null,
                    PostContentText = postContent.PostContentText,
                    PostContentTitle = postContent.PostContentTitle,
                    MediaId = postContent.Media == null ? 0 : postContent.Media.Id,
                    CreatedBy = postContent.CreatedBy,
                    CreatedDate = postContent.CreatedDate,
                    ModifiedBy = postContent.ModifiedBy,
                    ModifiedDate = postContent.ModifiedDate
                };
        }
    }
}
