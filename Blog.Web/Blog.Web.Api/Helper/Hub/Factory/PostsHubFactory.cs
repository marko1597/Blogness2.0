namespace Blog.Web.Api.Helper.Hub.Factory
{
    public class PostsHubFactory
    {
        private PostsHubFactory()
        {
        }

        private static PostsHubFactory _instance;

        public static PostsHubFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostsHubFactory();
                return _instance;
            }
            return _instance;
        }

        public PostsHub Create()
        {
            return new PostsHub();
        }
    }
}
