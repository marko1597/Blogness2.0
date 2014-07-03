using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class MediaMapper
    {
        public static Media ToDto(Db.Media media)
        {
            return media == null ? null : 
                new Media
                {
                    Id = media.MediaId,
                    MediaType = media.MediaType,
                    AlbumId = media.AlbumId,
                    MediaContent = null,
                    MediaPath = media.MediaPath,
                    MediaUrl = media.MediaUrl,
                    ThumbnailUrl = media.ThumbnailUrl,
                    ThumbnailPath = media.ThumbnailPath,
                    FileName = media.FileName,
                    CustomName = media.CustomName,
                    CreatedBy = media.CreatedBy,
                    CreatedDate = media.CreatedDate,
                    ModifiedBy = media.ModifiedBy,
                    ModifiedDate = media.ModifiedDate
                };
        }

        public static Db.Media ToEntity(Media media)
        {
            return media == null ? null : 
                new Db.Media
                {
                    MediaId = media.Id,
                    MediaType = media.MediaType,
                    AlbumId = media.AlbumId,
                    MediaPath = media.MediaPath,
                    MediaUrl = media.MediaUrl,
                    ThumbnailPath = media.ThumbnailPath,
                    ThumbnailUrl = media.ThumbnailUrl,
                    FileName = media.FileName,
                    CustomName = media.CustomName,
                    CreatedBy = media.CreatedBy,
                    CreatedDate = media.CreatedDate,
                    ModifiedBy = media.ModifiedBy,
                    ModifiedDate = media.ModifiedDate
                };
        }
    }
}
