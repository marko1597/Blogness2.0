using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
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

        public List<Media> GetByGroup(int albumId)
        {
            return MediaFactory.GetInstance().CreateMedia().GetByAlbum(albumId);
        }

        public Media GetByName(string customName)
        {
            return MediaFactory.GetInstance().CreateMedia().GetByName(customName);
        }

        public Media Add(Media media, int userId)
        {
            return MediaFactory.GetInstance().CreateMedia().Add(media, userId);
        }


        public Media Add(User user, string albumName, string filename, string path, string contentType)
        {
            return MediaFactory.GetInstance().CreateMedia().Add(user, albumName, filename, path, contentType);
        }

        public bool Delete(int mediaId)
        {
            return MediaFactory.GetInstance().CreateMedia().Delete(mediaId);
        }
    }
}
