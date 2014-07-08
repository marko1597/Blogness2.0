using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class AlbumService : BaseService, IAlbumService
    {
        private readonly IAlbumLogic _albumLogic;

        public AlbumService(IAlbumLogic albumLogic)
        {
            _albumLogic = albumLogic;
        }

        public List<Album> GetByUser(int userId)
        {
            return _albumLogic.GetByUser(userId);
        }

        public Album GetUserDefaultGroup(int userId)
        {
            return _albumLogic.GetUserDefaultGroup(userId);
        }

        public Album Add(Album album)
        {
            return _albumLogic.Add(album);
        }

        public Album Update(Album album)
        {
            return _albumLogic.Update(album);
        }

        public bool Delete(int albumId)
        {
            return _albumLogic.Delete(albumId);
        }
    }
}
