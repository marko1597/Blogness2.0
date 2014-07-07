using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<T> GetList()
        {
            return _cacheDataSource.GetList();
        }

        public List<T> GetListByKey(string key)
        {
            return _cacheDataSource.GetListByKey(key);
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

        public void SetListByKey(string key, List<T> value)
        {
            _cacheDataSource.SetListByKey(key, value);
        }

        public List<T> SetItemAndUpdateList(string key, T entity, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var list = _cacheDataSource.GetListByKey(key);

            if (list == null || list.Count == 0)
            {
                return null;
            }

            list.Add(entity);

            if (orderBy != null)
            {
                list = orderBy(list.AsQueryable()).ToList();
            }
            _cacheDataSource.SetListByKey(key, list);

            return _cacheDataSource.GetListByKey(key);
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
