using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blog.Backend.Common.Utils;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class MediaLogic
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IImageHelper _imageHelper;

        public MediaLogic(IMediaRepository mediaRepository, IImageHelper imageHelper)
        {
            _mediaRepository = mediaRepository;
            _imageHelper = imageHelper;
        }

        public List<Media> GetByUser(int userId)
        {
            var media = new List<Media>();
            try
            {
                var db = _mediaRepository.Find(a => a.UserId == userId, true).ToList();
                db.ForEach(a => media.Add(MediaMapper.ToDto(a, false)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public List<Media> GetByGroup(int mediaGroupId)
        {
            var media = new List<Media>();
            try
            {
                var db = _mediaRepository.Find(a => a.MediaGroupId == mediaGroupId, true).ToList();
                db.ForEach(a => media.Add(MediaMapper.ToDto(a, false)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public Media Get(int mediaId)
        {
            var media = new Media();
            try
            {
                return MediaMapper.ToDto(_mediaRepository.Find(a => a.MediaId == mediaId, true).FirstOrDefault(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public Media GetByName(string customName)
        {
            var media = new Media();
            try
            {
                return MediaMapper.ToDto(_mediaRepository.Find(a => a.CustomName == customName, true).FirstOrDefault(), true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public Media Add(Media media)
        {
            try
            {
                media.MediaPath = _imageHelper.GenerateImagePath(media.UserId, Constants.FileMediaLocation) + Path.GetFileName(media.FileName);
                media.ThumbnailPath = _imageHelper.GenerateImagePath(media.UserId, Constants.FileMediaLocation) + "tn\\" + Path.GetFileName(media.FileName);
                media.CustomName = Guid.NewGuid().ToString();
                media.MediaUrl = Constants.FileMediaUrl + media.CustomName;

                _imageHelper.CreateDirectory(media.MediaPath);
                var fs = new FileStream(media.MediaPath, FileMode.Create);
                fs.Write(media.MediaContent, 0, media.MediaContent.Length);

                if (media.MediaType != "image/gif" && media.MediaType.Substring(0, 5) != "video")
                {
                    _imageHelper.CreateThumbnailPath(media.ThumbnailPath);
                    media.ThumbnailContent = _imageHelper.CreateThumbnail(media.MediaPath);
                    media.ThumbnailUrl = Constants.FileMediaThumbnailUrl + media.CustomName;
                }

                if (media.MediaGroupId == 0)
                {
                    media.MediaGroupId =
                        MediaGroupFactory.GetInstance()
                            .CreateMediaGroup()
                            .GetUserDefaultGroup(media.UserId)
                            .MediaGroupId;
                }

                _mediaRepository.Add(MediaMapper.ToEntity(media));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            return media;
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
