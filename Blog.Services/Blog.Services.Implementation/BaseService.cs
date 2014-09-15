using System.ServiceModel.Activation;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public abstract class BaseService : IBaseService
    {
        public bool GetHeartBeat()
        {
            return true;
        }
    }
}
