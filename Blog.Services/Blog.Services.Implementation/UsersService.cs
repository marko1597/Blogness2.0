using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class UsersService : BaseService, IUsersService
    {
        private readonly IUsersLogic _usersLogic;

        public UsersService(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        public List<User> GetUsers(int threshold = 10, int skip = 10)
        {
            return _usersLogic.GetUsers(threshold, skip);
        }

        public List<User> GetUsersWithNoIdentityId()
        {
            return _usersLogic.GetUsersWithNoIdentityId();
        }

        public User GetByUserName(string username)
        {
            return _usersLogic.GetByUserName(username);
        }

        public User GetByIdentityId(string identityId)
        {
            return _usersLogic.GetByIdentity(identityId);
        }

        public User Get(int userId)
        {
            return _usersLogic.Get(userId);
        }

        public User Add(User user)
        {
            return _usersLogic.Add(user);
        }

        public User Update(User user)
        {
            return _usersLogic.Update(user);
        }
    }
}
