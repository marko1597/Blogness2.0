using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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
            IMediaRepository mediaRepository = new MediaRepository();
            return new MediaLogic(mediaRepository);
        }
    }
}
