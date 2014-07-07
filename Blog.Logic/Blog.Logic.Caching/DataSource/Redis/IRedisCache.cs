namespace Blog.Logic.Caching.DataSource.Redis
{
    public interface IRedisCache<T> : ICacheDataSource<T> where T : class
    {
    }
}
