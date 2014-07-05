namespace Blog.Logic.Caching.DataSource
{
    public interface ICacheDataSource<T> : ICache<T> where T : class
    {
    }
}
