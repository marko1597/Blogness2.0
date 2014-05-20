namespace Blog.Backend.Common.Web.Authentication
{
    public class AuthenticationApiFactory
    {
        private AuthenticationApiFactory()
        {
        }

        private static AuthenticationApiFactory _instance;

        public static AuthenticationApiFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AuthenticationApiFactory();
                return _instance;
            }
            return _instance;
        }

        public AuthenticationApi Create()
        {
            return new AuthenticationApi();
        }
    }
}
