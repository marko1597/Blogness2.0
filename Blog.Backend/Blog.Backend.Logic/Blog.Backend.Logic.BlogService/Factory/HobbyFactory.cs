using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class HobbyFactory
    {
        private HobbyFactory()
        {
        }

        private static HobbyFactory _instance;

        public static HobbyFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new HobbyFactory();
                return _instance;
            }
            return _instance;
        }

        public Hobby CreateHobby()
        {
            IHobbyResource hobbyResource = new HobbyResource();
            return new Hobby(hobbyResource);
        }
    }
}
