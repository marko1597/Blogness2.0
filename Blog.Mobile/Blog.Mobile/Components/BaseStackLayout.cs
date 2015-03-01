using Xamarin.Forms;

namespace Blog.Mobile.Components
{
	public class BaseStackLayout : StackLayout
	{
		public BaseStackLayout ()
		{
			BackgroundColor = Color.White;
            Padding = new Thickness(10);
			VerticalOptions = LayoutOptions.FillAndExpand;
			HorizontalOptions = LayoutOptions.FillAndExpand;
		}
	}
}

