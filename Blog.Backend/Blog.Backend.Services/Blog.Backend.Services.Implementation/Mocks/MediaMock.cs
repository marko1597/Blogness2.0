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
        public MediaMock()
        {
            if (DataStorage.Media.Count == 0)
            {
                var localIpAddress = string.Empty;
                var mediaId = 1;

                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
                {
                    localIpAddress = ip.ToString();
                    break;
                }

                foreach (var u in DataStorage.Users)
                {
                    var u1 = u;
                    var albums = DataStorage.Albums.FindAll(a => a.User.UserId == u1.UserId);

                    for (var i = 1; i < 6; i++)
                    {
                        var albumName = i < 4 ? "Stuff" : "Miscellaneous";
                        var mediaPath = "C:\\SampleImages\\" + u.UserId + "\\" + albumName + "\\" + i + ".jpg";
                        var tnPath = "C:\\SampleImages\\" + u.UserId + "\\" + albumName + "\\tn\\" + i + ".jpg";
                        var image = Image.FromFile(mediaPath);
                        var customName = Guid.NewGuid().ToString();

                        if (i < 4)
                        {
                            DataStorage.Media.Add(new Media
                            {
                                MediaId = mediaId,
                                CustomName = customName,
                                CreatedBy = u.UserId,
                                CreatedDate = DateTime.UtcNow,
                                ModifiedBy = u.UserId,
                                ModifiedDate = DateTime.UtcNow,
                                AlbumId = albums[0].AlbumId,
                                FileName = i + ".jpg",
                                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", localIpAddress, customName),
                                MediaType = "image/jpeg",
                                MediaPath = mediaPath,
                                MediaContent = new ImageHelper().ImageToByteArray(image),
                                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/thumbnail/{1}", localIpAddress, customName),
                                ThumbnailPath = tnPath,
                                ThumbnailContent = new ImageHelper().CreateThumbnail(mediaPath)
                            });
                        }
                        else
                        {
                            DataStorage.Media.Add(new Media
                            {
                                MediaId = mediaId,
                                CustomName = customName,
                                CreatedBy = u.UserId,
                                CreatedDate = DateTime.UtcNow,
                                ModifiedBy = u.UserId,
                                ModifiedDate = DateTime.UtcNow,
                                AlbumId = albums[1].AlbumId,
                                FileName = i + ".jpg",
                                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", localIpAddress, customName),
                                MediaType = "image/jpeg",
                                MediaPath = mediaPath,
                                MediaContent = new ImageHelper().ImageToByteArray(image),
                                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/thumbnail/{1}", localIpAddress, customName),
                                ThumbnailPath = tnPath,
                                ThumbnailContent = new ImageHelper().CreateThumbnail(mediaPath)
                            });
                        }

                        mediaId++;
                    }
                }
            }
        }
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
