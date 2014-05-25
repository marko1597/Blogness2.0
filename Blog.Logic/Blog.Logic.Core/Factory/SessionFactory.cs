using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
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
            IUserRepository userRepository = new UserRepository();
            return new SessionLogic(sessionRepository, userRepository);
        }
    }
}
