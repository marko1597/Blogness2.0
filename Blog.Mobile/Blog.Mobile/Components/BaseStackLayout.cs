using Xamarin.Forms;

namespace Blog.Mobile.Components
{
	public class BaseStackLayout : StackLayout
	{
		public BaseStackLayout ()
		{
            Padding = new Thickness(10);
			VerticalOptions = LayoutOptions.FillAndExpand;
			HorizontalOptions = LayoutOptions.FillAndExpand;
		}
	}
}

