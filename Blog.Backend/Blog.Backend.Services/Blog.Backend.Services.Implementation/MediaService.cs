using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
{
    public class MediaService : IMedia
    {
        public List<Media> GetByUser(int userId)
        {
            return MediaFactory.GetInstance().CreateMedia().GetByUser(userId);
        }

        public Media Get(int mediaId)
        {
            return MediaFactory.GetInstance().CreateMedia().Get(mediaId);
        }

        public List<Media> GetByGroup(int mediaGroupId)
        {
            return MediaFactory.GetInstance().CreateMedia().GetByGroup(mediaGroupId);
        }

        public Media GetByName(string customName)
        {
            return MediaFactory.GetInstance().CreateMedia().GetByName(customName);
        }

        public Media Add(Media media)
        {
            return MediaFactory.GetInstance().CreateMedia().Add(media);
        }

        public bool Delete(int mediaId)
        {
            return MediaFactory.GetInstance().CreateMedia().Delete(mediaId);
        }
    }
}
