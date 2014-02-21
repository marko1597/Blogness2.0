using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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

        public CommentLikesLogic CreateCommentLikes()
        {
            ICommentLikeRepository commentLikeRepository = new CommentLikeRepository();
            return new CommentLikesLogic(commentLikeRepository);
        }
    }
}
