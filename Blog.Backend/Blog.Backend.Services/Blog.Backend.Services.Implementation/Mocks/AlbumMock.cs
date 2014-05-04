using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class AlbumMock : IAlbum
    {
        public AlbumMock()
        {
            if (DataStorage.Albums.Count == 0)
            {
                var albumId = 1;

                foreach (var u in DataStorage.Users)
                {
                    DataStorage.Albums.Add(new Album
                    {
                        AlbumId = albumId,
                        AlbumName = "Stuff",
                        User = u,
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow,
                        IsUserDefault = false
                    });
                    albumId++;

                    DataStorage.Albums.Add(new Album
                    {
                        AlbumId = albumId,
                        AlbumName = "Miscellaneous",
                        User = u,
                        CreatedBy = u.UserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedBy = u.UserId,
                        ModifiedDate = DateTime.UtcNow,
                        IsUserDefault = true
                    });
                    albumId++;
                }
            }
        }
        public List<Album> GetByUser(int userId)
        {
            var albums = DataStorage.Albums.FindAll(a => a.User.UserId == userId);
            return albums;
        }

        public Album GetUserDefaultGroup(int userId)
        {
            var album = DataStorage.Albums.FirstOrDefault(a => a.User.UserId == userId && a.IsUserDefault);
            return album;
        }

        public bool Add(Album album)
        {
            var id = DataStorage.Albums.Select(a => a.AlbumId).Max();
            album.AlbumId = id + 1;
            DataStorage.Albums.Add(album);

            return true;
        }

        public bool Update(Album album)
        {
            var tAlbum = DataStorage.Albums.FirstOrDefault(a => a.AlbumId == album.AlbumId);
            DataStorage.Albums.Remove(tAlbum);
            DataStorage.Albums.Add(album);

            return true;
        }

        public bool Delete(int albumId)
        {
            var tAlbum = DataStorage.Albums.FirstOrDefault(a => a.AlbumId == albumId);
            DataStorage.Albums.Remove(tAlbum);

            return true;
        }
    }
}
