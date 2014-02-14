using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class SessionFactory
    {
        private SessionFactory()
        {
        }

        private static SessionFactory _instance;

        public static SessionFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SessionFactory();
                return _instance;
            }
            return _instance;
        }

        public SessionLogic CreateSession()
        {
            ISessionResource sessionResource = new SessionResource();
            return new SessionLogic(sessionResource);
        }
    }
}
