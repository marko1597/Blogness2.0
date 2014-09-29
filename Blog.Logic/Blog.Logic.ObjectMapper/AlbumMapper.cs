using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class AlbumMapper
    {
        public static Album ToDto(Db.Album album)
        {
            if (album != null)
            {
                var media = new List<Media>();
                if (album.Media != null)
                {
                    media = album.Media.Select(MediaMapper.ToDto).ToList();
                }
                
                return new Album
                {
                    AlbumId = album.AlbumId,
                    AlbumName = album.AlbumName,
                    Media = media,
                    User = UserMapper.ToDto(album.User),
                    IsUserDefault = album.IsUserDefault,
                    CreatedBy = album.CreatedBy,
                    CreatedDate = album.CreatedDate,
                    ModifiedBy = album.ModifiedBy,
                    ModifiedDate = album.ModifiedDate
                };
            }
            return null;
        }

        public static Db.Album ToEntity(Album album)
        {
            if (album != null)
            {
                var media = new List<DataAccess.Database.Entities.Objects.Media>();
                if (album.Media != null)
                {
                    media = album.Media.Select(MediaMapper.ToEntity).ToList();
                }

                return new Db.Album
                {
                    AlbumId = album.AlbumId,
                    AlbumName = album.AlbumName,
                    Media = media,
                    UserId = album.User.Id,
                    IsUserDefault = album.IsUserDefault,
                    CreatedBy = album.CreatedBy,
                    CreatedDate = album.CreatedDate,
                    ModifiedBy = album.ModifiedBy,
                    ModifiedDate = album.ModifiedDate
                };
            }
            return null;
        }
    }
}
