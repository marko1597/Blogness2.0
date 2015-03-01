using Blog.Mobile.Components;
using Xamarin.Forms;

namespace Blog.Mobile.Views.Posts
{
    public class PostsListView : BaseContentPage
    {
        public PostsListView()
        {
            var stack = new BaseStackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };

            var about = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "Posts",
                LineBreakMode = LineBreakMode.WordWrap
            };

            stack.Children.Add(about);

            Content = stack;
        }
    }
}
