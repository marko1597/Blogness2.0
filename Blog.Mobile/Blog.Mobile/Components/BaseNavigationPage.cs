using Xamarin.Forms;
using Color = Blog.Mobile.Helpers.Color;

namespace Blog.Mobile.Components
{
	public class BaseNavigationPage : NavigationPage
	{
		public BaseNavigationPage (Page page) : base(page)
		{
			BarBackgroundColor = Color.DarkGray.ToFormsColor ();
			BarTextColor = Color.White.ToFormsColor ();
		}
	}
}

