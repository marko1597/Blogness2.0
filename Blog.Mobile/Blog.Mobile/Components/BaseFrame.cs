using Xamarin.Forms;
using Color = Blog.Mobile.Helpers.Color;

namespace Blog.Mobile.Components
{
	public class BaseFrame : Frame
	{
		public BaseFrame ()
		{
			BackgroundColor = Color.LightGray.ToFormsColor ();
			Padding = 0;
			HasShadow = true;
			VerticalOptions = LayoutOptions.FillAndExpand;
		}
	}
}

