using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService.Factory
{
    public class EducationFactory
    {
        private EducationFactory()
        {
        }

        private static EducationFactory _instance;

        public static EducationFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EducationFactory();
                return _instance;
            }
            return _instance;
        }

        public EducationLogic CreateEducation()
        {
            IEducationResource educationResource = new EducationResource();
            return new EducationLogic(educationResource);
        }
    }
}
