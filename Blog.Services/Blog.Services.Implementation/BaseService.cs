using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public abstract class BaseService : IBaseService
    {
        public bool GetHeartBeat()
        {
            return true;
        }
    }
}
