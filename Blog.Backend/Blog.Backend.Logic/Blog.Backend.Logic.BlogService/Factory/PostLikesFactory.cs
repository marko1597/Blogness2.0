using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class PostLikesFactory
    {
        private PostLikesFactory()
        {
        }

        private static PostLikesFactory _instance;

        public static PostLikesFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PostLikesFactory();
                return _instance;
            }
            return _instance;
        }

        public PostLikes CreatePostLikes()
        {
            IPostLikeResource postLikeResource = new PostLikeResource();
            return new PostLikes(postLikeResource);
        }
    }
}
