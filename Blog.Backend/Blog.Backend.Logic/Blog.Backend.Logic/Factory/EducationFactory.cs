using Blog.Backend.DataAccess.Repository;

namespace Blog.Backend.Logic.Factory
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
            IEducationRepository educationRepository = new EducationRepository();
            return new EducationLogic(educationRepository);
        }
    }
}
