using Blog.DataAccess.Database.Repository;
using Blog.DataAccess.Database.Repository.Interfaces;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.Core.Factory
{
    public class TagsFactory
    {
        private TagsFactory()
        {
        }

        private static TagsFactory _instance;

        public static TagsFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TagsFactory();
                return _instance;
            }
            return _instance;
        }

        public TagsLogic CreateLogic()
        {
            ITagRepository tagRepository = new TagRepository();
            IPostRepository postRepository = new PostRepository();
            return new TagsLogic(tagRepository, postRepository);
        }
    }
}
