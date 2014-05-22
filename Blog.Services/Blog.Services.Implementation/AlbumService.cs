using System.Collections.Generic;
using Blog.Logic.Core.Factory;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation
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
