using Blog.Mobile.ViewModels;
using Xamarin.Forms;

namespace Blog.Mobile.Components
{
    public class BaseTabbedPage : TabbedPage
    {
        public BaseTabbedPage()
        {
            SetBinding(TitleProperty, new Binding(BaseViewModel.TitlePropertyName));
            SetBinding(IconProperty, new Binding(BaseViewModel.IconPropertyName));
        }
    }
}
