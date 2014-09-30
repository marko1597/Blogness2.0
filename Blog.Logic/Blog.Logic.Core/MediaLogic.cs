using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;
using Media = Blog.Common.Contracts.Media;

namespace Blog.Logic.Core
{
    public class MediaLogic : IMediaLogic
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

        public Media Add(Media media, int userId)
        {
            try
            {
                var guid = Guid.NewGuid().ToString();
                var extension = !string.IsNullOrEmpty(media.MediaType) ? media.MediaType.Split('/')[1] ?? "jpg" : "jpg";
                var filename = guid + "." + extension;

                var album = _albumRepository.Find(a => a.AlbumId == media.AlbumId, false).FirstOrDefault();
                if (album == null)
                {
                    album = GetAlbumByName(DateTime.Now.ToShortDateString(), userId);
                    if (album == null) throw new Exception("Error creating or finding album");
                }

                var mediaPath = _imageHelper.GenerateImagePath(album.UserId, guid, Constants.FileMediaLocation);
                if (string.IsNullOrEmpty(mediaPath)) throw new Exception("Error generating media directory path");

                var hasCreatedDir = _fileHelper.CreateDirectory(mediaPath);
                if (!hasCreatedDir) throw new Exception("Error creating media directory");

                var hasCreatedMedia = _imageHelper.SaveImage(media.MediaContent, mediaPath, filename);
                if (!hasCreatedMedia) throw new Exception("Error saving media");

                var tMedia = PrepareMediaForAdding(filename, album.AlbumId, mediaPath, album.UserId, media.MediaType, guid);
                CreateThumbnail(tMedia, mediaPath, filename);

                return MediaMapper.ToDto(_mediaRepository.Add(MediaMapper.ToEntity(media)));
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
                var guid = Guid.NewGuid().ToString();

                var album = GetAlbumByName(albumName, user.Id);
                if (album == null) throw new Exception("Error creating or finding album");

                var mediaPath = _imageHelper.GenerateImagePath(user.Id, guid, Constants.FileMediaLocation);
                if (string.IsNullOrEmpty(mediaPath)) throw new Exception("Error generating media directory path");

                var hasCreatedDir = _fileHelper.CreateDirectory(mediaPath);
                if (!hasCreatedDir) throw new Exception("Error creating media directory");

                var hasSuccessfullyMovedMedia = _fileHelper.MoveFile(path, mediaPath + "\\" + filename);
                if (!hasSuccessfullyMovedMedia) throw new Exception("Error moving media to correct directory");

                var media = PrepareMediaForAdding(filename, album.AlbumId, mediaPath, user.Id, contentType, guid);
                CreateThumbnail(media, mediaPath, filename);

                return MediaMapper.ToDto(_mediaRepository.Add(MediaMapper.ToEntity(media)));
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
                var db = _mediaRepository.Find(a => a.MediaId == mediaId, false).FirstOrDefault();
                if (db == null) return false;

                // TODO: Removed for now as it causes privilege issues
                //_fileHelper.DeleteFile(db.ThumbnailPath);
                //_fileHelper.DeleteFile(db.MediaPath);
                //_fileHelper.DeleteDirectory(db.ThumbnailPath);
                //_fileHelper.DeleteDirectory(db.MediaPath);
                _mediaRepository.Delete(db);

                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
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
                                            && a.UserId == userId, false).FirstOrDefault()
                : _albumRepository.Find(a => a.IsUserDefault
                                            && a.UserId == userId, false).FirstOrDefault();

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
