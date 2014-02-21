using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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
            ISessionRepository sessionRepository = new SessionRepository();
            return new SessionLogic(sessionRepository);
        }
    }
}
