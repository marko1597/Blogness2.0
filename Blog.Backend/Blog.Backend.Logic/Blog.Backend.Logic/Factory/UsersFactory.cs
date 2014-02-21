using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
{
    public class UsersFactory
    {
        private UsersFactory()
        {
        }

        private static UsersFactory _instance;

        public static UsersFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UsersFactory();
                return _instance;
            }
            return _instance;
        }

        public UsersLogic CreateUsers()
        {
            IUserRepository userRepository = new UserRepository();
            return new UsersLogic(userRepository);
        }
    }
}
