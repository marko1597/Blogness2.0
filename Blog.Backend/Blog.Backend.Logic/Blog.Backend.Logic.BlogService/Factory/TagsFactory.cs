using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
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

        public Tags CreateTags()
        {
            ITagResource tagResource = new TagResource();
            return new Tags(tagResource);
        }
    }
}
