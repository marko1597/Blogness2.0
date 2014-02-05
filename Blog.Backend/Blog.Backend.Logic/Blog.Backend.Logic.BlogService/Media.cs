using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Blog.Backend.Common.BlogService;
using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService
{
    public class Media
    {
        private readonly IMediaResource _mediaResource;
        private readonly IMediaGroupResource _mediaGroupResource;

        public Media(IMediaResource mediaResource, IMediaGroupResource mediaGroupResource)
        {
            _mediaResource = mediaResource;
            _mediaGroupResource = mediaGroupResource;
        }

        public List<Services.BlogService.Contracts.BlogObjects.Media> GetByUser(int userId)
        {
            var media = new List<Services.BlogService.Contracts.BlogObjects.Media>();
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

        public Services.BlogService.Contracts.BlogObjects.Media Get(int mediaId)
        {
            var media = new Services.BlogService.Contracts.BlogObjects.Media();
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

        public Services.BlogService.Contracts.BlogObjects.Media Add(Services.BlogService.Contracts.BlogObjects.Media media)
        {
            var newMedia = new Services.BlogService.Contracts.BlogObjects.Media();

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
                    media.MediaGroupId = _mediaGroupResource.Get(a => a.IsUserDefault && a.UserId == media.UserId).First().MediaGroupId;
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
