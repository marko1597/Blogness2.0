using Blog.Common.Utils;
using Blog.DataAccess.Database.Repository;

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
            return new MediaLogic(mediaRepository, albumRepository, imageHelper);
        }
    }
}
