using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class PostContentMapper
    {
        public static PostContent ToDto(DataAccess.Database.Entities.Objects.PostContent postContent)
        {
            return postContent == null ?
                new PostContent() :
                new PostContent
                {
                    PostContentId = postContent.PostContentId,
                    PostId = postContent.PostId,
                    Media = postContent.Media != null ? MediaMapper.ToDto(postContent.Media) : new Media(),
                    PostContentText = postContent.PostContentText,
                    PostContentTitle = postContent.PostContentTitle,
                    CreatedBy = postContent.CreatedBy,
                    CreatedDate = postContent.CreatedDate,
                    ModifiedBy = postContent.ModifiedBy,
                    ModifiedDate = postContent.ModifiedDate
                };
        }

        public static DataAccess.Database.Entities.Objects.PostContent ToEntity(PostContent postContent)
        {
            return postContent == null ?
                new DataAccess.Database.Entities.Objects.PostContent() :
                new DataAccess.Database.Entities.Objects.PostContent
                {
                    PostContentId = postContent.PostContentId,
                    PostId = postContent.PostId,
                    Media = null,
                    PostContentText = postContent.PostContentText,
                    PostContentTitle = postContent.PostContentTitle,
                    MediaId = postContent.Media != null ? postContent.Media.MediaId : 0,
                    CreatedBy = postContent.CreatedBy,
                    CreatedDate = postContent.CreatedDate,
                    ModifiedBy = postContent.ModifiedBy,
                    ModifiedDate = postContent.ModifiedDate
                };
        }
    }
}
