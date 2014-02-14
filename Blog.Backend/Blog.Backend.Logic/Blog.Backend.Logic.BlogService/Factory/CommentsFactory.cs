using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class CommentsFactory
    {
        private CommentsFactory()
        {
        }

        private static CommentsFactory _instance;

        public static CommentsFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CommentsFactory();
                return _instance;
            }
            return _instance;
        }

        public CommentsLogic CreateCommentLikes()
        {
            ICommentResource commentResource = new CommentResource();
            return new CommentsLogic(commentResource);
        }
    }
}