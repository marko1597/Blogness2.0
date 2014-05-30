using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
{
    public class MediaFactory
    {
        private MediaFactory()
        {
        }

        private static MediaFactory _instance;

        public static MediaFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MediaFactory();
                return _instance;
            }
            return _instance;
        }

        public MediaLogic CreateMedia()
        {
            IAlbumRepository albumRepository = new AlbumRepository();
            IMediaRepository mediaRepository = new MediaRepository();
            IImageHelper imageHelper = new ImageHelper();
            IConfigurationHelper configurationHelper = new ConfigurationHelper();
            return new MediaLogic(mediaRepository, albumRepository, imageHelper, configurationHelper);
        }
    }
}
