using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Logic.BlogService
{
    public class MediaGroupLogic
    {
        private readonly IMediaGroupResource _mediaGroupResource;

        public MediaGroupLogic(IMediaGroupResource mediaGroupResource)
        {
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
                    Media = MediaFactory.GetInstance().CreateMedia().GetByGroup(g.MediaGroupId)
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userMediaGroup;
        }

        public MediaGroup GetUserDefaultGroup(int userId)
        {
            var mediaGroup = new MediaGroup();
            try
            {
                mediaGroup = _mediaGroupResource.Get(a => a.IsUserDefault && a.UserId == userId).First();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mediaGroup;
        }

        public void Delete(MediaGroup mediaGroup)
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

        public MediaGroup Add(MediaGroup mediaGroup)
        {
            try
            {
                return _mediaGroupResource.Add(mediaGroup);
            }
            catch
            {
                return new MediaGroup();
            }
        }

        public MediaGroup Update(MediaGroup mediaGroup)
        {
            try
            {
                return _mediaGroupResource.Update(mediaGroup);
            }
            catch
            {
                return new MediaGroup();
            }
        }
    }
}
