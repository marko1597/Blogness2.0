using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class MediaService : BaseService, IMediaService
    {
        private readonly IMediaLogic _mediaLogic;

        public MediaService(IMediaLogic mediaLogic)
        {
            _mediaLogic = mediaLogic;
        }

        public List<Media> GetByUser(int userId)
        {
            return _mediaLogic.GetByUser(userId);
        }

        public Media Get(int mediaId)
        {
            return _mediaLogic.Get(mediaId);
        }

        public List<Media> GetByGroup(int albumId)
        {
            return _mediaLogic.GetByAlbum(albumId);
        }

        public Media GetByName(string customName)
        {
            return _mediaLogic.GetByName(customName);
        }

        public Media Add(Media media, int userId)
        {
            return _mediaLogic.Add(media, userId);
        }
        
        public Media AddAsContent(User user, string albumName, string filename, string path, string contentType)
        {
            return _mediaLogic.Add(user, albumName, filename, path, contentType);
        }

        public bool Delete(int mediaId)
        {
            return _mediaLogic.Delete(mediaId);
        }
    }
}
