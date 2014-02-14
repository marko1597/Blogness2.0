using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class PostsFactory
    {
        private PostsFactory()
        {
        }

        private static PostsFactory _instance;

        public static PostsFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostsFactory();
                return _instance;
            }
            return _instance;
        }

        public PostsLogic CreatePosts()
        {
            IPostTagResource postTagResource = new PostTagResource();
            IPostResource postResource = new PostResource();
            return new PostsLogic(postResource, postTagResource);
        }
    }
}
