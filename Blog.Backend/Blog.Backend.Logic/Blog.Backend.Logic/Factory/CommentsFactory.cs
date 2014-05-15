using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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

        public CommentsLogic CreateComments()
        {
            ICommentRepository commentRepository = new CommentRepository();
            return new CommentsLogic(commentRepository);
        }
    }
}