using Blog.Mobile.Components;
using Blog.Mobile.Services;
using Blog.Mobile.ViewModels.Login;
using Xamarin.Forms;

namespace Blog.Mobile.Views.Login
{
    public class LoginView : BaseContentPage
    {
        public LoginView()
        {
            BindingContext = new LoginViewModel(new AccountService(), Navigation, this);

            var stack = new BaseStackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10
            };

            var loginHeader = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                Text = "Bloggity Login",
                LineBreakMode = LineBreakMode.WordWrap
            };

            var usernameEntry = new CustomTextEntry("Username") {Placeholder = "Username"};
            var passwordEntry = new CustomTextEntry("Password") {Placeholder = "Password", IsPassword = true};
            var loginButton = new CustomButton { Text = "Login" };
            loginButton.SetBinding(Button.CommandProperty, "LoginCommand");

            stack.Children.Add(loginHeader);
            stack.Children.Add(usernameEntry);
            stack.Children.Add(passwordEntry);
            stack.Children.Add(loginButton);

            Content = stack;
        }
    }
}
