namespace Blog.Frontend.Common.Authentication
{
    public class ApiFactory
    {
        private ApiFactory()
        {
        }

        private static ApiFactory _instance;

        public static ApiFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ApiFactory();
                return _instance;
            }
            return _instance;
        }

        public Api CreateApi()
        {
            return new Api();
        }
    }
}
