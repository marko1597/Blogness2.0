using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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
            IMediaGroupRepository mediaGroupRepository = new MediaGroupRepository();
            return new MediaGroupLogic(mediaGroupRepository);
        }
    }
}
