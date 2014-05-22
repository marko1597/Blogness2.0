using Blog.DataAccess.Database.Repository;

namespace Blog.Logic.Core.Factory
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

        public PostLikesLogic CreatePostLikes()
        {
            IPostLikeRepository postLikeRepository = new PostLikeRepository();
            return new PostLikesLogic(postLikeRepository);
        }
    }
}
