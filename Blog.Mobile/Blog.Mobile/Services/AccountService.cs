using System.Threading.Tasks;

namespace Blog.Mobile.Services
{
    public class AccountService : IAccountService
    {
        public async Task<bool> Login(string username, string password)
        {
            var result = await Task<bool>.Factory.StartNew(() => username == "test" && password == "test");
            return result;
        }
    }
}