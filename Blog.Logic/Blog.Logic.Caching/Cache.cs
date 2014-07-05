using System.Collections.Generic;
using Blog.Logic.Caching.DataSource;

namespace Blog.Logic.Caching
{
    public class Cache<T> : ICache<T> where T : class
    {
        private readonly ICacheDataSource<T> _cacheDataSource;

        public Cache(ICacheDataSource<T> cacheDataSource)
        {
            _cacheDataSource = cacheDataSource;
        }

        public List<T> GetAll()
        {
            return _cacheDataSource.GetAll();
        }

        public T Get(int id, string key)
        {
            return _cacheDataSource.Get(id, key);
        }

        public T Get(string name, string key)
        {
            return _cacheDataSource.Get(name, key);
        }

        public List<string> GetKeys()
        {
            return _cacheDataSource.GetKeys();
        }

        public void SetAll(List<T> entities)
        {
            _cacheDataSource.SetAll(entities);
        }

        public void Set(T entity)
        {
            _cacheDataSource.Set(entity);
        }

        public void RemoveAll()
        {
            _cacheDataSource.RemoveAll();
        }

        public void Remove(int id, string key)
        {
            _cacheDataSource.Remove(id, key);
        }

        public void Remove(string name, string key)
        {
            _cacheDataSource.Remove(name, key);
        }

        public void Replace(string key, T entity)
        {
            _cacheDataSource.Replace(key, entity);
        }
    }
}
