using System.Collections.ObjectModel;
using Blog.Mobile.Models.Home;

namespace Blog.Mobile.ViewModels.Home
{
	public class HomeViewModel : BaseViewModel
	{
		public ObservableCollection<HomeMenuItem> MenuItems { get; set; }

		public HomeViewModel()
		{
			CanLoadMore = true;
			Title = "Bloggity";

			MenuItems = new ObservableCollection<HomeMenuItem>
			{
				new HomeMenuItem
				{
					Id = 0,
					Title = "Posts",
					MenuType = HomeMenuType.Posts
				},
				new HomeMenuItem
				{
					Id = 1,
					Title = "People",
					MenuType = HomeMenuType.People
				},
				new HomeMenuItem
				{
					Id = 2,
					Title = "Profile",
					MenuType = HomeMenuType.Profile
				},
				new HomeMenuItem
				{
					Id = 3,
					Title = "Communities",
					MenuType = HomeMenuType.Communities
				},
				new HomeMenuItem
				{
					Id = 4,
					Title = "Events",
					MenuType = HomeMenuType.Events
				},
				new HomeMenuItem
				{
					Id = 5,
					Title = "Messages",
					MenuType = HomeMenuType.Messages
				}
			};
		}
	}
}
