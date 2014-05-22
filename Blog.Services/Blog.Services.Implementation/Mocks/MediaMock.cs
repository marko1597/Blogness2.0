using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class MediaMock : IMedia
    {
        public List<Media> GetByUser(int userId)
        {
            var media = new List<Media>();
            var albums = DataStorage.Albums.FindAll(a => a.User.UserId == userId);
            albums.ForEach(a => media.AddRange(DataStorage.Media.FindAll(b => b.AlbumId == a.AlbumId)));

            return media;
        }

        public List<Media> GetByGroup(int albumId)
        {
            var media = new List<Media>();
            var albums = DataStorage.Albums.FindAll(a => a.AlbumId == albumId);
            albums.ForEach(a => media.AddRange(DataStorage.Media.FindAll(b => b.AlbumId == a.AlbumId)));

            return media;
        }

        public Media GetByName(string customName)
        {
            var media = DataStorage.Media.FirstOrDefault(a => a.CustomName == customName);
            return media;
        }

        public Media Get(int mediaId)
        {
            var media = DataStorage.Media.FirstOrDefault(a => a.MediaId == mediaId);
            return media;
        }

        public Media Add(Media media)
        {
            var id = DataStorage.Media.Select(a => a.MediaId).Max();
            media.MediaId = id + 1;
            DataStorage.Media.Add(media);

            return media;
        }

        public Media Add(User user, string albumName, string filename, string path, string contentType)
        {
            var id = DataStorage.Media.Select(a => a.MediaId).Max();
            var media = new Media { MediaId = id + 1 };
            DataStorage.Media.Add(media);

            return media;
        }

        public bool Delete(int mediaId)
        {
            var tMedia = DataStorage.Media.FirstOrDefault(a => a.MediaId == mediaId);
            DataStorage.Media.Remove(tMedia);

            return true;
        }
    }
}
