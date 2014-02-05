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

        public Posts CreatePosts()
        {
            IPostResource postResource = new PostResource();
            IPostTagResource postTagResource = new PostTagResource();
            ITagResource tagResource = new TagResource();
            IPostContentResource postContentResource = new PostContentResource();
            return new Posts(postResource, postTagResource, postContentResource, tagResource);
        }
    }
}
