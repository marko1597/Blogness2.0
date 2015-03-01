using System.Threading.Tasks;
using Blog.Mobile.Services;
using Xamarin.Forms;

namespace Blog.Mobile.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAccountService _accountService;
        private readonly INavigation _navigation;
        private readonly Page _page;

        string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public LoginViewModel(IAccountService accountService, INavigation navigation, Page page)
        {
            _accountService = accountService;
            _navigation = navigation;
            _page = page;

            Title = "Bloggity Login";
        }

        private Command _loginCommand;
        public Command LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new Command(async () => await ExecuteLoginCommand()));
            }
        }

        protected async Task ExecuteLoginCommand()
        {
            var result = await _accountService.Login(_username, _password);
            if (result)
            {
                App.IsLoggedIn = true;
                await _navigation.PopModalAsync();
            }
            else
            {
                await _page.DisplayAlert("Error!", "Username or password is incorrect.", "Ok");
            }
        }
    }
}
