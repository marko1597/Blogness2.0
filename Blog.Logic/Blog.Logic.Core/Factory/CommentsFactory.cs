using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
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