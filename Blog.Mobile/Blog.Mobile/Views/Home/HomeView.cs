using System.Collections.Generic;
using Blog.Mobile.Components;
using Blog.Mobile.Models.Home;
using Blog.Mobile.ViewModels.Home;
using Blog.Mobile.Views.Login;
using Xamarin.Forms;

namespace Blog.Mobile.Views.Home
{
    public class HomeView : MasterDetailPage
    {
        HomeViewModel ViewModel
        {
            get { return BindingContext as HomeViewModel; }
        }

        readonly HomeMasterView _master;
        private readonly Dictionary<HomeMenuType, NavigationPage> _pages;

        public HomeView()
        {
            _pages = new Dictionary<HomeMenuType, NavigationPage>();
            BindingContext = new HomeViewModel();

            Master = _master = new HomeMasterView(ViewModel);

			var homeNav = new BaseNavigationPage (_master.PageSelection);
            Detail = homeNav;

            _pages.Add(HomeMenuType.Posts, homeNav);

            _master.PageSelectionChanged = async menuType =>
            {
                if (Detail != null && Device.OS == TargetPlatform.WinPhone)
                {
                    await Detail.Navigation.PopToRootAsync();
                }

                NavigationPage newPage;

                if (_pages.ContainsKey(menuType))
                {
                    newPage = _pages[menuType];
                }
                else
                {
					newPage = new BaseNavigationPage (_master.PageSelection);
                    _pages.Add(menuType, newPage);
                }
                Detail = newPage;
                Detail.Title = _master.PageSelection.Title;
                IsPresented = false;
            };

            Icon = "slideout.png";
			IsLoggedIn ();
        }

		async void IsLoggedIn()
		{
			if (!App.IsLoggedIn) 
			{
				await Navigation.PushModalAsync (new LoginView ());
			}
		}
    }
}