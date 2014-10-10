using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Utils.Helpers.Interfaces;
using ServiceStack.Redis;

namespace Blog.Logic.Caching.DataSource.Redis
{
    [ExcludeFromCodeCoverage]
    public class RedisCache<T> : IRedisCache<T> where T : class
    {
        private readonly IConfigurationHelper _configurationHelper;
        private readonly IErrorSignaler _errorSignaler;

        public RedisCache(IConfigurationHelper configurationHelper, IErrorSignaler errorSignaler)
        {
            _configurationHelper = configurationHelper;
            _errorSignaler = errorSignaler;
        }

        public T GetEntry(string key)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var redis = client.As<T>();
                    var result = redis.GetValue(key);

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public T GetEntry(int id)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var key = string.Format("{0}:{1}", GetTypeName(typeof(T)), id);
                    var redis = client.As<T>();
                    var result = redis.GetValue(key);

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<T> GetEntriesAsList()
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var stringKey = string.Format("{0}:*", GetTypeName(typeof(T)));
                    var keys = client.ScanAllKeys(stringKey).ToList();
                    var list = client.GetValues<T>(keys);

                    return list;
                }
            }
            catch
            {
                return null;
            }
        }

        public void SetEntry(T entity, string key)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var redis = client.As<T>();
                    redis.SetEntry(key, entity);
                }
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void SetEntry(T entity, int id)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var key = string.Format("{0}:{1}", GetTypeName(typeof(T)), id);
                    var redis = client.As<T>();
                    redis.SetEntry(key, entity);
                }
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void ReplaceEntry(T entity, string key)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var redis = client.As<T>();
                    redis.SetEntry(key, entity);
                }
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void ReplaceEntry(T entity, int id)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var key = string.Format("{0}:{1}", GetTypeName(typeof (T)), id);
                    var redis = client.As<T>();
                    redis.SetEntry(key, entity);
                }
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void RemoveEntry(string key)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var redis = client.As<T>();
                    redis.RemoveEntry(key);
                }
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public void RemoveEntry(int id)
        {
            try
            {
                using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
                {
                    var key = string.Format("{0}:{1}", GetTypeName(typeof (T)), id);
                    var redis = client.As<T>();
                    redis.RemoveEntry(key);
                }
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }

        public List<T> GetList()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];
                return list.ToList();
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];

                if (filter == null) return list.ToList();

                var query = list.ToList().AsQueryable().Where(filter);
                return query.ToList();
            }
        }

        public List<T> GetList(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];

                if (orderBy == null) return list.ToList();

                IQueryable<T> query = new EnumerableQuery<T>(list);
                query = orderBy(query);
                return query.ToList();
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];
                IQueryable<T> query = new EnumerableQuery<T>(list);

                if (filter != null)
                {
                    query = list.ToList().AsQueryable().Where(filter);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                return query.ToList();
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter, int threshold, int skip)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];
                IQueryable<T> query = new EnumerableQuery<T>(list);

                if (filter != null)
                {
                    query = list.ToList().AsQueryable()
                        .Where(filter)
                        .Take(threshold)
                        .Skip(skip);
                }

                return query.ToList();
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int threshold, int skip)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];
                IQueryable<T> query = new EnumerableQuery<T>(list);

                if (filter != null)
                {
                    query = list.ToList().AsQueryable()
                        .Where(filter).Take(threshold).Skip(skip);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                return query.ToList();
            }
        }

        public List<string> GetKeys()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var entity = client.GetAllKeys();
                return entity;
            }
        }

        public void SetList(List<T> entities)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];

                foreach (var e in entities)
                {
                    list.Add(e);
                }
            }
        }

        public void AddToList(T entity)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];

                list.Add(entity);
            }
        }

        public void ClearList()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetTypeName(typeof(T))];
                list.RemoveAll();
            }
        }

        public void RemoveAll()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.FlushAll();
            }
        }

        private static string GetTypeName(Type src)
        {
            return string.Format("{0}s", src.Name.ToLower());
        }
    }
}
