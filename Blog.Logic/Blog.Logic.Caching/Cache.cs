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

        public T GetEntry(string key)
        {
            try
            {
                return _cacheDataSource.GetEntry(key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public T GetEntry(int id)
        {
            try
            {
                return _cacheDataSource.GetEntry(id);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<T> GetEntriesAsList()
        {
            try
            {
                return _cacheDataSource.GetEntriesAsList();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void SetEntry(T entity, string key)
        {
            try
            {
                _cacheDataSource.SetEntry(entity, key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void SetEntry(T entity, int id)
        {
            try
            {
                _cacheDataSource.SetEntry(entity, id);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void ReplaceEntry(T entity, string key)
        {
            try
            {
                _cacheDataSource.ReplaceEntry(entity, key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void ReplaceEntry(T entity, int id)
        {
            try
            {
                _cacheDataSource.ReplaceEntry(entity, id);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void RemoveEntry(string key)
        {
            try
            {
                _cacheDataSource.RemoveEntry(key);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void RemoveEntry(int id)
        {
            try
            {
                _cacheDataSource.RemoveEntry(id);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
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

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            try
            {
                return _cacheDataSource.GetList(filter);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<T> GetList(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            try
            {
                return _cacheDataSource.GetList(orderBy);
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

        public List<T> GetList(Expression<Func<T, bool>> filter, int threshold, int skip)
        {
            try
            {
                return _cacheDataSource.GetList(filter, threshold, skip);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int threshold, int skip)
        {
            try
            {
                return _cacheDataSource.GetList(filter, orderBy, threshold, skip);
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

        public void SetList(List<T> entities)
        {
            try
            {
                _cacheDataSource.SetList(entities);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void AddToList(T entity)
        {
            try
            {
                _cacheDataSource.AddToList(entity);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public void ClearList()
        {
            try
            {
                _cacheDataSource.ClearList();
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
    }
}
