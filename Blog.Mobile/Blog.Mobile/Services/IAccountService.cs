using System.Threading.Tasks;

namespace Blog.Mobile.Services
{
    public interface IAccountService
    {
        Task<bool> Login(string username, string password);
    }
}
