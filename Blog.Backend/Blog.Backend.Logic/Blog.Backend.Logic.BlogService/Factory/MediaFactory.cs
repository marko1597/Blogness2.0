using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
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
            IMediaResource mediaResource = new MediaResource();
            return new MediaLogic(mediaResource);
        }
    }
}
