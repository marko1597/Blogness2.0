using Xamarin.Forms;

namespace Blog.Mobile.Components
{
	public class CustomTextEntry : Entry
	{
		public CustomTextEntry ()
		{
		}

		public CustomTextEntry(string binding)
		{
			this.SetBinding(TextProperty, binding);
		}
	}
}