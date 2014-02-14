using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Blog.Backend.Common.BlogService;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class MediaLogic
    {
        private readonly IMediaResource _mediaResource;

        public MediaLogic(IMediaResource mediaResource)
        {
            _mediaResource = mediaResource;
        }

        public List<Media> GetByUser(int userId)
        {
            var media = new List<Media>();
            try
            {
                media = _mediaResource.Get(a => a.UserId == userId);
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
                media = _mediaResource.Get(a => a.MediaGroupId == mediaGroupId);
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
                media = _mediaResource.Get(a => a.MediaId == mediaId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return media;
        }

        public Media Add(Media media)
        {
            var newMedia = new Media();

            try
            {
                Utils.CreateDirectory(media.MediaPath);
                var fs = new FileStream(media.MediaPath, FileMode.Create);
                fs.Write(media.MediaContent, 0, media.MediaContent.Length);

                if (media.MediaType != "image/gif" && media.MediaType.Substring(0, 5) != "video")
                {
                    Utils.CreateThumbnailPath(media.ThumbnailPath);
                    media.ThumbnailContent = Utils.CreateThumbnail(media.MediaPath);
                }

                if (media.MediaGroupId == 0)
                {
                    media.MediaGroupId =
                        MediaGroupFactory.GetInstance()
                            .CreateMediaGroup()
                            .GetUserDefaultGroup(media.UserId)
                            .MediaGroupId;
                }

                newMedia = _mediaResource.Add(media);
                newMedia.ExternalUrl = ConfigurationManager.AppSettings["MediaExternalUrl"] + newMedia.MediaId;
                _mediaResource.Update(newMedia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newMedia;
        }

        public void Delete(int mediaId)
        {
            try
            {
                var media = _mediaResource.Get(a => a.MediaId == mediaId).FirstOrDefault();
                if (media != null)
                {
                    _mediaResource.Delete(media);
                    File.Delete(media.ThumbnailPath);
                    File.Delete(media.MediaPath);
                    Utils.DeleteThumbnailPath(media.ThumbnailPath);
                    Utils.DeleteDirectory(media.MediaPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
