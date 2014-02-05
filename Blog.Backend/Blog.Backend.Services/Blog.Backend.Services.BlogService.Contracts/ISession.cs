using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface ISession
    {
        Session GetByUser(int userId);
    }
}
