using Blog.Mobile.Views.Home;
using Xamarin.Forms;

namespace Blog.Mobile
{
    public class App : Application
    {
        public static INavigation Navigation { get; set; }

		public static bool IsLoggedIn;

        public static string AuthenticationToken { get; set; }

		public App()
		{
			MainPage = GetMainPage ();
		}

		public static Page GetMainPage ()
		{
			return new HomeView ();
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
