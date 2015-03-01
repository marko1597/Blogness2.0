using Xamarin.Forms;

namespace Blog.Mobile.Components
{
	public class BaseScrollView : ScrollView
	{
		public BaseScrollView()
		{
			VerticalOptions = LayoutOptions.FillAndExpand;
			HorizontalOptions = LayoutOptions.FillAndExpand;
		}
	}
}