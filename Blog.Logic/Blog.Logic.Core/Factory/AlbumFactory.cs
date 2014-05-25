using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
{
    public class AlbumFactory
    {
        private AlbumFactory()
        {
        }

        private static AlbumFactory _instance;

        public static AlbumFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AlbumFactory();
                return _instance;
            }
            return _instance;
        }

        public AlbumLogic CreateAlbumLogic()
        {
            IAlbumRepository albumRepository = new AlbumRepository();
            return new AlbumLogic(albumRepository);
        }
    }
}
