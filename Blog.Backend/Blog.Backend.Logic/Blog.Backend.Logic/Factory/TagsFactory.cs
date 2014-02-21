using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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

        public TagsLogic CreateTags()
        {
            ITagRepository tagRepository = new TagRepository();
            return new TagsLogic(tagRepository);
        }
    }
}
