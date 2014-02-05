using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class Session : ISession
    {
        public Contracts.BlogObjects.Session GetByUser(int userId)
        {
            return SessionFactory.GetInstance().CreateSession().GetByUser(userId);
        }
    }
}
