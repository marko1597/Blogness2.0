using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Utils;

namespace Blog.Backend.Services.Implementation.Mocks
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

        public bool Delete(int mediaId)
        {
            var tMedia = DataStorage.Media.FirstOrDefault(a => a.MediaId == mediaId);
            DataStorage.Media.Remove(tMedia);

            return true;
        }
    }
}
