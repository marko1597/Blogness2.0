using System.Collections.Generic;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public class AlbumService : IAlbum
    {
        public List<Album> GetByUser(int userId)
        {
            return AlbumFactory.GetInstance().CreateAlbumLogic().GetByUser(userId);
        }

        public Album GetUserDefaultGroup(int userId)
        {
            return AlbumFactory.GetInstance().CreateAlbumLogic().GetUserDefaultGroup(userId);
        }

        public bool Add(Album album)
        {
            return AlbumFactory.GetInstance().CreateAlbumLogic().Add(album);
        }

        public bool Update(Album album)
        {
            return AlbumFactory.GetInstance().CreateAlbumLogic().Update(album);
        }

        public bool Delete(int albumId)
        {
            return AlbumFactory.GetInstance().CreateAlbumLogic().Delete(albumId);
        }
    }
}
