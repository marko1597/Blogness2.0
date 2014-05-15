using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Backend.Common.Contracts.Utils;
using Blog.Backend.Common.Utils;
using Blog.Backend.DataAccess.Entities.Objects;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;
using Media = Blog.Backend.Common.Contracts.Media;

namespace Blog.Backend.Logic
{
    public class MediaLogic
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IImageHelper _imageHelper;

        public MediaLogic(IMediaRepository mediaRepository, IAlbumRepository albumRepository, IImageHelper imageHelper)
        {
            _mediaRepository = mediaRepository;
            _albumRepository = albumRepository;
            _imageHelper = imageHelper;
        }

        public List<Media> GetByUser(int userId)
        {
            var media = new List<Media>();
            try
            {
                var album = _albumRepository.Find(a => a.UserId == userId, null, "User,Media").ToList();
                album.ForEach(a => media.AddRange(a.Media.Select(MediaMapper.ToDto)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return media;
        }

        public List<Media> GetByGroup(int albumId)
        {
            var media = new List<Media>();
            try
            {
                var db = _mediaRepository.Find(a => a.AlbumId == albumId, true).ToList();
                db.ForEach(a => media.Add(MediaMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return media;
        }

        public Media Get(int mediaId)
        {
            try
            {
                return MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == mediaId).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Media GetByName(string customName)
        {
            try
            {
                return MediaMapper.ToDto(_mediaRepository.Find(a => a.CustomName == customName, true).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Media Add(Media media)
        {
            try
            {
                var guid = Guid.NewGuid().ToString();
                var album = _albumRepository.Find(a => a.AlbumId == media.AlbumId, null, "User").FirstOrDefault();
                if (album == null) return null;

                media.MediaPath = _imageHelper.GenerateImagePath(album.UserId, album.AlbumName, guid, Constants.FileMediaLocation);
                media.ThumbnailPath = _imageHelper.GenerateImagePath(album.UserId, album.AlbumName, guid, Constants.FileMediaLocation) + "tn\\";
                media.CustomName = Guid.NewGuid().ToString();
                media.MediaUrl = Constants.FileMediaUrl + media.CustomName;

                _imageHelper.CreateDirectory(media.MediaPath);
                var fs = new FileStream(media.MediaPath + media.FileName, FileMode.Create);
                fs.Write(media.MediaContent, 0, media.MediaContent.Length);

                if (media.MediaType != "image/gif" && media.MediaType.Substring(0, 5) != "video")
                {
                    _imageHelper.CreateThumbnailPath(media.ThumbnailPath);
                    media.ThumbnailUrl = Constants.FileMediaThumbnailUrl + media.CustomName;
                }

                if (media.AlbumId == 0)
                {
                    media.AlbumId =
                        AlbumFactory.GetInstance()
                            .CreateAlbumLogic()
                            .GetUserDefaultGroup(album.UserId)
                            .AlbumId;
                }

                var result = _mediaRepository.Add(MediaMapper.ToEntity(media));
                return MediaMapper.ToDto(result);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Media Add(Common.Contracts.User user, string albumName, string filename, string path, string contentType)
        {
            try
            {
                var album = GetAlbumByName(albumName, user.UserId);
                var guid = Guid.NewGuid().ToString();
                var mediaPath = _imageHelper.GenerateImagePath(user.UserId, album.AlbumName, guid, Constants.FileMediaLocation);
                var hasCreatedDir = _imageHelper.CreateDirectory(mediaPath);

                if (!hasCreatedDir) return null;

                if (!string.IsNullOrEmpty(filename))
                {
                    filename = filename.Substring(1, filename.Length - 2);
                    if (path != null)
                    {
                        File.Move(path, mediaPath + "\\" + filename);
                    }
                }

                var tMedia = new Media
                             {
                                 FileName = filename,
                                 AlbumId = album.AlbumId,
                                 MediaPath = mediaPath,
                                 ThumbnailPath = mediaPath + "tn\\",
                                 CustomName = Guid.NewGuid().ToString(),
                                 CreatedBy = user.UserId,
                                 CreatedDate = DateTime.UtcNow,
                                 ModifiedBy = user.UserId,
                                 ModifiedDate = DateTime.UtcNow
                             };
                tMedia.ThumbnailUrl = Constants.FileMediaThumbnailUrl + tMedia.CustomName;
                tMedia.MediaUrl = Constants.FileMediaUrl + tMedia.CustomName;
                tMedia.MediaType = contentType;

                if (IsMediaSupported(tMedia.MediaType))
                {
                    _imageHelper.CreateThumbnailPath(tMedia.ThumbnailPath);

                    if (IsVideo(tMedia.MediaType))
                    {
                        Task.Run(() => _imageHelper.CreateVideoThumbnail(mediaPath + "\\" + filename, tMedia.ThumbnailPath));
                    }
                    else if (tMedia.MediaType == "image/gif")
                    {
                        Task.Run(() => _imageHelper.CreateGifThumbnail(mediaPath + "\\" + filename, tMedia.ThumbnailPath));
                    }
                    else
                    {
                        Task.Run(() => _imageHelper.CreateThumbnail(mediaPath + "\\" + filename, tMedia.ThumbnailPath));
                    }
                    
                    tMedia.ThumbnailUrl = Constants.FileMediaThumbnailUrl + tMedia.CustomName;
                }

                var result = _mediaRepository.Add(MediaMapper.ToEntity(tMedia));
                return MediaMapper.ToDto(result);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int mediaId)
        {
            try
            {
                var db = _mediaRepository.Find(a => a.MediaId == mediaId, true).FirstOrDefault();
                if (db == null) return false;

                _mediaRepository.Delete(db);
                File.Delete(db.ThumbnailPath);
                File.Delete(db.MediaPath);
                _imageHelper.DeleteThumbnailPath(db.ThumbnailPath);
                _imageHelper.DeleteDirectory(db.MediaPath);

                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private Album GetAlbumByName(string albumName, int userId)
        {
            var album = albumName.ToLower() != "default"
                ? _albumRepository.Find(a => a.AlbumName.ToLower() == albumName.ToLower()
                                            && a.UserId == userId).FirstOrDefault()
                : _albumRepository.Find(a => a.IsUserDefault
                                            && a.UserId == userId).FirstOrDefault();

            if (album == null)
            {
                var tAlbum = _albumRepository.Add(new Album
                {
                    AlbumName = albumName,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.UtcNow,
                    UserId = userId,
                    IsUserDefault = false
                });

                return tAlbum;
            }

            return album;
        }

        private bool IsMediaSupported(string mimeType)
        {
            var supportedMedia = new List<string>
            {
                "image/bmp",
                "image/x-windows-bmp",
                "image/jpeg",
                "image/png",
                "image/tiff",
                "image/x-tiff",
                "image/gif",
                "video/avi",
                "video/quicktime",
                "video/mpeg",
                "video/mp4",
                "video/x-flv"
            };

            return supportedMedia.Contains(mimeType);
        }

        private bool IsVideo(string mimeType)
        {
            var supportedMedia = new List<string>
            {
                "video/avi",
                "video/quicktime",
                "video/mpeg",
                "video/mp4",
                "video/x-flv"
            };

            return supportedMedia.Contains(mimeType);
        }
    }
}
