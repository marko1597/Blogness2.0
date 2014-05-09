using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class MediaMapper
    {
        public static Media ToDto(DataAccess.Entities.Objects.Media media)
        {
            return media == null ?
                new Media() : 
                new Media
                {
                    MediaId = media.MediaId,
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

        public static DataAccess.Entities.Objects.Media ToEntity(Media media)
        {
            return media == null ?
                new DataAccess.Entities.Objects.Media() :
                new DataAccess.Entities.Objects.Media
                {
                    MediaId = media.MediaId,
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
