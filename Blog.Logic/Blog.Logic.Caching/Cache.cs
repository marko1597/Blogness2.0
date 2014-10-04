using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils.Extensions;
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
            try
            {
                return _cacheDataSource.GetList();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            try
            {
                return _cacheDataSource.GetList(filter, orderBy);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<T> GetListByKey(string key)
        {
            try
            {
                return _cacheDataSource.GetListByKey(key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public T Get(int id, string key)
        {
            try
            {
                return _cacheDataSource.Get(id, key);

            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public T Get(string name, string key)
        {
            try
            {
                return _cacheDataSource.Get(name, key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<string> GetKeys()
        {
            try
            {
                return _cacheDataSource.GetKeys();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void SetAll(List<T> entities)
        {
            try
            {
                _cacheDataSource.SetAll(entities);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void Set(T entity)
        {
            try
            {
                _cacheDataSource.Set(entity);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void SetListByKey(string key, List<T> value)
        {
            try
            {
                _cacheDataSource.SetListByKey(key, value);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<T> SetItemAndUpdateList(string key, T entity, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try
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
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void RemoveAll()
        {
            try
            {
                _cacheDataSource.RemoveAll();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void Remove(int id, string key)
        {
            try
            {
                _cacheDataSource.Remove(id, key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void Remove(string name, string key)
        {
            try
            {
                _cacheDataSource.Remove(name, key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void Replace(string key, T entity)
        {
            try
            {
                _cacheDataSource.Replace(key, entity);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
