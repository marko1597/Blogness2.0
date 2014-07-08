using System.Diagnostics.CodeAnalysis;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseResource : IBaseResource
    {
        public bool GetHeartBeat()
        {
            return true;
        }
    }
}
