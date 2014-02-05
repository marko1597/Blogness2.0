using System;
using System.Collections.Generic;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Logic.BlogService
{
    public class MediaGroup
    {
        private readonly IMediaResource _mediaResource;
        private readonly IMediaGroupResource _mediaGroupResource;

        public MediaGroup(IMediaResource mediaResource, IMediaGroupResource mediaGroupResource)
        {
            _mediaResource = mediaResource;
            _mediaGroupResource = mediaGroupResource;
        }

        public List<UserMediaGroup> GetByUser(int userId)
        {
            var userMediaGroup = new List<UserMediaGroup>();
            try
            {
                var groups = _mediaGroupResource.Get(a => a.UserId == userId);
                groups.ForEach(g => userMediaGroup.Add(new UserMediaGroup
                {
                    MediaGroupId = g.MediaGroupId,
                    MediaGroupName = g.MediaGroupName,
                    Media = _mediaResource.Get(m => m.MediaGroupId == g.MediaGroupId && m.UserId == g.UserId)
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userMediaGroup;
        }

        public void Delete(Services.BlogService.Contracts.BlogObjects.MediaGroup mediaGroup)
        {
            try
            {
                _mediaGroupResource.Delete(mediaGroup);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        public Services.BlogService.Contracts.BlogObjects.MediaGroup Add(Services.BlogService.Contracts.BlogObjects.MediaGroup mediaGroup)
        {
            try
            {
                return _mediaGroupResource.Add(mediaGroup);
            }
            catch
            {
                return new Services.BlogService.Contracts.BlogObjects.MediaGroup();
            }
        }

        public Services.BlogService.Contracts.BlogObjects.MediaGroup Update(Services.BlogService.Contracts.BlogObjects.MediaGroup mediaGroup)
        {
            try
            {
                return _mediaGroupResource.Update(mediaGroup);
            }
            catch
            {
                return new Services.BlogService.Contracts.BlogObjects.MediaGroup();
            }
        }
    }
}
