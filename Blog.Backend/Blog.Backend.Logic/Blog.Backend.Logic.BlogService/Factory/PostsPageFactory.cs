using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class PostsPageFactory
    {
        private PostsPageFactory()
        {
        }

        private static PostsPageFactory _instance;

        public static PostsPageFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostsPageFactory();
                return _instance;
            }
            return _instance;
        }

        public PostsPageLogic CreatePostsPage()
        {
            IPostResource postResource = new PostResource();
            return new PostsPageLogic(postResource);
        }
    }
}
