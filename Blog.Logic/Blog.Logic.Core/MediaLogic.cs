using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Factory;
using Blog.Logic.ObjectMapper;
using Media = Blog.Common.Contracts.Media;

namespace Blog.Logic.Core
{
    public class MediaLogic
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IImageHelper _imageHelper;
        private readonly IConfigurationHelper _configurationHelper;
        private readonly IFileHelper _fileHelper;

        public MediaLogic(IMediaRepository mediaRepository, IAlbumRepository albumRepository, 
            IImageHelper imageHelper, IConfigurationHelper configurationHelper, IFileHelper fileHelper)
        {
            _mediaRepository = mediaRepository;
            _albumRepository = albumRepository;
            _imageHelper = imageHelper;
            _configurationHelper = configurationHelper;
            _fileHelper = fileHelper;
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

        public List<Media> GetByAlbum(int albumId)
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
                var db = _mediaRepository.Find(a => a.MediaId == mediaId, true).FirstOrDefault();

                if (db != null)
                {
                    return MediaMapper.ToDto(db);
                }

                return new Media().GenerateError<Media>((int) Constants.Error.RecordNotFound,
                    string.Format("Media with Id {0} not found", mediaId));
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
                var db = _mediaRepository.Find(a => a.CustomName == customName, true).FirstOrDefault();

                if (db != null)
                {
                    return MediaMapper.ToDto(db);
                }

                return new Media().GenerateError<Media>((int)Constants.Error.RecordNotFound,
                    string.Format("Media with name {0} not found", customName));
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

                _fileHelper.CreateDirectory(media.MediaPath);
                var fs = new FileStream(media.MediaPath + media.FileName, FileMode.Create);
                fs.Write(media.MediaContent, 0, media.MediaContent.Length);

                if (media.MediaType != "image/gif" && media.MediaType.Substring(0, 5) != "video")
                {
                    _fileHelper.CreateDirectory(media.ThumbnailPath);
                    media.ThumbnailUrl = Constants.FileMediaUrl + media.CustomName + @"/thumb";
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
                filename = filename.Substring(1, filename.Length - 2);
                var guid = Guid.NewGuid().ToString();

                var album = GetAlbumByName(albumName, user.UserId);
                if (album == null) throw new Exception("Error creating or finding album");

                var mediaPath = _imageHelper.GenerateImagePath(user.UserId, album.AlbumName, guid, Constants.FileMediaLocation);
                if (string.IsNullOrEmpty(mediaPath)) throw new Exception("Error generating media directory path");

                var hasCreatedDir = _fileHelper.CreateDirectory(mediaPath);
                if (!hasCreatedDir) throw new Exception("Error creating media directory");

                var hasSuccessfullyMovedMedia = MoveMediaFileToCorrectPath(filename, path, mediaPath);
                if (!hasSuccessfullyMovedMedia) throw new Exception("Error moving media to correct directory");

                var media = PrepareMediaForAdding(filename, album.AlbumId, mediaPath, user.UserId, contentType, guid);
                CreateThumbnail(media, mediaPath, filename);

                var result = _mediaRepository.Add(MediaMapper.ToEntity(media));
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
                _fileHelper.DeleteDirectory(db.ThumbnailPath);
                _fileHelper.DeleteDirectory(db.MediaPath);

                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private bool MoveMediaFileToCorrectPath(string filename, string path, string mediaPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filename))
                {
                    if (path != null)
                    {
                        _fileHelper.MoveFile(path, mediaPath + "\\" + filename);
                        return true;
                    }
                    throw new Exception("Path name is empty");
                }
                throw new Exception("File name is empty");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private static Media PrepareMediaForAdding(string filename, int albumId, string mediaPath, int userId, string contentType, string customName)
        {
            var media = new Media
            {
                FileName = filename,
                AlbumId = albumId,
                MediaPath = mediaPath,
                ThumbnailPath = mediaPath + "tn\\",
                CustomName = customName,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                ModifiedBy = userId,
                ModifiedDate = DateTime.Now
            };
            media.ThumbnailUrl = Constants.FileMediaUrl + media.CustomName + @"/thumb";
            media.MediaUrl = Constants.FileMediaUrl + media.CustomName;
            media.MediaType = contentType;

            return media;
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
                    CreatedDate = DateTime.Now,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.Now,
                    UserId = userId,
                    IsUserDefault = false
                });

                return tAlbum;
            }

            return album;
        }

        private void CreateThumbnail(Media media, string mediaPath, string filename)
        {
            if (IsMediaSupported(media.MediaType))
            {
                _fileHelper.CreateDirectory(media.ThumbnailPath);

                if (IsVideo(media.MediaType))
                {
                    Task.Run(() => _imageHelper.CreateVideoThumbnail(mediaPath + filename, media.ThumbnailPath,
                        _configurationHelper.GetAppSettings("ThumbnailPrefix")));
                }
                else if (media.MediaType == "image/gif")
                {
                    Task.Run(() => _imageHelper.CreateGifThumbnail(mediaPath + filename, media.ThumbnailPath, 
                        _configurationHelper.GetAppSettings("ThumbnailPrefix")));
                }
                else
                {
                    Task.Run(() => _imageHelper.CreateThumbnail(mediaPath + filename, media.ThumbnailPath,
                        _configurationHelper.GetAppSettings("ThumbnailPrefix")));
                }
            }
        }

        private static bool IsMediaSupported(string mimeType)
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

        private static bool IsVideo(string mimeType)
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
