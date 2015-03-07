using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class UsersRestResource : IUsersRestResource
    {
        public User GetByUserName(string username)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<User>(
                    svc.Get(Constants.BlogRestUrl, string.Format("users/{0}", username)));
                return result;
            }
        }

        public User Get(int userId)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<User>(
                    svc.Get(Constants.BlogRestUrl, string.Format("users/{0}", userId)));
                return result;
            }
        }

        public User Add(User user, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<User>(
                    svc.Post(Constants.BlogRestUrl, "users", user, authenticationToken));
                return result;
            }
        }

        public User Update(User user, string authenticationToken)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<User>(
                    svc.Put(Constants.BlogRestUrl, "users", user, authenticationToken));
                return result;
            }
        }
    }
}
