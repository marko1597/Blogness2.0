using Blog.Mobile.ViewModels;
using Xamarin.Forms;

namespace Blog.Mobile.Components
{
    public class BaseContentPage : ContentPage
    {
		public BaseContentPage()
		{
			SetBinding(TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
			SetBinding(IconProperty, new Binding(BaseViewModel.IconPropertyName));

			Device.OnPlatform (
				iOS: () => {
					BackgroundImage = "background.png";
				},
				Android: () => {
					BackgroundImage = "@drawable/background";
				},
				WinPhone: () => {
					BackgroundImage = "Images/background.png";
				}
			);
		}
    }
}
