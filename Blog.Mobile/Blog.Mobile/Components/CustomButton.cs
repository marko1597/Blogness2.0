using Xamarin.Forms;
using Color = Blog.Mobile.Helpers.Color;

namespace Blog.Mobile.Components
{
    public class CustomButton : Button
    {
        public CustomButton()
        {
            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            BackgroundColor = Color.LightBlue.ToFormsColor();
            // ReSharper disable once ImpureMethodCallOnReadonlyValueField
            TextColor = Color.White.ToFormsColor();
            HorizontalOptions = LayoutOptions.CenterAndExpand;
        }
    }
}
