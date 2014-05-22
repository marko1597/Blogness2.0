using Blog.DataAccess.Database.Repository;

namespace Blog.Logic.Core.Factory
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

        public PostsPageLogic CreatePostsPage()
        {
            IPostRepository postRepository = new PostRepository();
            return new PostsPageLogic(postRepository);
        }
    }
}
