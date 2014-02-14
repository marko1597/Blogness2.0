using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class PostContentsFactory
    {
        private PostContentsFactory()
        {
        }

        private static PostContentsFactory _instance;

        public static PostContentsFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostContentsFactory();
                return _instance;
            }
            return _instance;
        }

        public PostContentsLogic CreatePostContents()
        {
            IPostContentResource postContentResource = new PostContentResource();
            return new PostContentsLogic(postContentResource);
        }
    }
}
