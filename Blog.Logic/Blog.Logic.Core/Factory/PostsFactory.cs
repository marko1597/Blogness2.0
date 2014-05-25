using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
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
            IPostRepository postRepository = new PostRepository();
            return new PostsLogic(postRepository);
        }
    }
}
