using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class MediaGroupFactory
    {
        private MediaGroupFactory()
        {
        }

        private static MediaGroupFactory _instance;

        public static MediaGroupFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MediaGroupFactory();
                return _instance;
            }
            return _instance;
        }

        public MediaGroupLogic CreateMediaGroup()
        {
            IMediaGroupResource mediaGroupResource = new MediaGroupResource();
            return new MediaGroupLogic(mediaGroupResource);
        }
    }
}
