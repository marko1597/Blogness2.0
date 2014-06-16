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

        public PostsLogic CreateLogic()
        {
            IPostRepository postRepository = new PostRepository();
            IPostContentRepository postContentRepository = new PostContentRepository();
            ITagRepository tagRepository = new TagRepository();
            ICommentRepository commentRepository = new CommentRepository();
            IMediaRepository mediaRepository = new MediaRepository();
            return new PostsLogic(postRepository, postContentRepository, tagRepository, commentRepository, mediaRepository);
        }
    }
}
