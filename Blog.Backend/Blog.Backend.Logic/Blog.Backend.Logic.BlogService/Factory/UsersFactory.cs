using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
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

        public Users CreateUsers()
        {
            IUserResource userResource = new UserResource();
            ISessionResource sessionResource = new SessionResource();
            return new Users(userResource, sessionResource);
        }
    }
}
