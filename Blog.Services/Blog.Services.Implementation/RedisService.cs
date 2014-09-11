using Blog.Common.Utils.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace Blog.Services.Implementation
{
    public class RedisService : IRedisService
    {
        private readonly IConfigurationHelper _configurationHelper;

        private RedisClient _redisClient;
        public RedisClient RedisClient
        {
            get
            {
                return _redisClient ?? new RedisClient(_configurationHelper.GetAppSettings("RedisServer"));
            }
            set { _redisClient = value; }
        }

        public RedisService(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public void Publish<T>(T message) where T : class
        {
            using (var redisPublisher = RedisClient)
            {
                redisPublisher.PublishMessage(_configurationHelper.GetAppSettings("BlogRedisChannel"),
                    JsonConvert.SerializeObject(message));
            }
        }
    }
}
