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

        public PostsPage CreatePostsPage()
        {
            IPostResource postResource = new PostResource();
            IUserResource userResource = new UserResource();
            IPostContentResource postContentResource = new PostContentResource();
            return new PostsPage(userResource, postResource, postContentResource);
        }
    }
}
