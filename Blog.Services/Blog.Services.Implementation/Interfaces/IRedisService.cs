namespace Blog.Services.Implementation.Interfaces
{
    public interface IRedisService
    {
        void Publish<T>(T message) where T : class;
    }
}
