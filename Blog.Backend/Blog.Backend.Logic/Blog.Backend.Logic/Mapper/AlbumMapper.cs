using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class AlbumMapper
    {
        public static Album ToDto(DataAccess.Entities.Objects.Album album)
        {
            if (album != null)
            {
                var media = new List<Media>();
                if (album.Media != null)
                {
                    media = album.Media.Select(a => MediaMapper.ToDto(a, false)).ToList();
                }
                
                return new Album
                {
                    AlbumId = album.AlbumId,
                    AlbumName = album.AlbumName,
                    Media = media,
                    User = UserMapper.ToDto(album.User, true),
                    IsUserDefault = album.IsUserDefault,
                    CreatedBy = album.CreatedBy,
                    CreatedDate = album.CreatedDate,
                    ModifiedBy = album.ModifiedBy,
                    ModifiedDate = album.ModifiedDate
                };
            }
            return new Album();
        }

        public static DataAccess.Entities.Objects.Album ToEntity(Album album)
        {
            if (album != null)
            {
                var media = new List<DataAccess.Entities.Objects.Media>();
                if (album.Media != null)
                {
                    media = album.Media.Select(MediaMapper.ToEntity).ToList();
                }

                return new DataAccess.Entities.Objects.Album
                {
                    AlbumId = album.AlbumId,
                    AlbumName = album.AlbumName,
                    Media = media,
                    User = UserMapper.ToEntity(album.User),
                    IsUserDefault = album.IsUserDefault,
                    CreatedBy = album.CreatedBy,
                    CreatedDate = album.CreatedDate,
                    ModifiedBy = album.ModifiedBy,
                    ModifiedDate = album.ModifiedDate
                };
            }
            return new DataAccess.Entities.Objects.Album();
        }
    }
}
