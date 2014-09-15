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
    public class HobbyService : BaseService, IHobbyService
    {
        private readonly IHobbyLogic _hobbyLogic;

        public HobbyService(IHobbyLogic hobbyLogic)
        {
            _hobbyLogic = hobbyLogic;
        }

        public List<Hobby> GetByUser(int userId)
        {
            return _hobbyLogic.GetByUser(userId);
        }

        public Hobby Add(Hobby hobby)
        {
            return _hobbyLogic.Add(hobby);
        }

        public Hobby Update(Hobby hobby)
        {
            return _hobbyLogic.Update(hobby);
        }

        public bool Delete(int hobbyId)
        {
            return _hobbyLogic.Delete(hobbyId);
        }
    }
}
