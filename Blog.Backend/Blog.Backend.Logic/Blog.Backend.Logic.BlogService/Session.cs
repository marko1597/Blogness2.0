using System;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService
{
    public class Session
    {
        private readonly ISessionResource _sessionResource;

        public Session(ISessionResource sessionResource)
        {
            _sessionResource = sessionResource;
        }

        public Services.BlogService.Contracts.BlogObjects.Session GetByUser(int userId)
        {
            try
            {
                CleanupExpiredSessions();
                var session = _sessionResource.Get(a => a.UserId == userId).FirstOrDefault();

                return session ?? new Services.BlogService.Contracts.BlogObjects.Session();
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Session();
            }
        }

        private void CleanupExpiredSessions()
        {
            var oldSessions = _sessionResource.Get(a => a.TimeValidity <= DateTime.UtcNow);

            oldSessions.ForEach(a => _sessionResource.Delete(a.UserId));
        }
    }
}
