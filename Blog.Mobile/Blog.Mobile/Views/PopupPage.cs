using Xamarin.Forms;

namespace Blog.Mobile.Views
{
	public class PopupPage : ContentPage
	{
		public PopupPage ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

