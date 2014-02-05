using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class CommentLikesFactory
    {
        private CommentLikesFactory()
        {
        }

        private static CommentLikesFactory _instance;

        public static CommentLikesFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommentLikesFactory();
                return _instance;
            }
            return _instance;
        }

        public CommentLikes CreateCommentLikes()
        {
            ICommentLikeResource commentLikeResource = new CommentLikeResource();
            return new CommentLikes(commentLikeResource);
        }
    }
}
