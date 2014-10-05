using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils.Helpers.Interfaces;
using ServiceStack.Redis;

namespace Blog.Logic.Caching.DataSource.Redis
{
    [ExcludeFromCodeCoverage]
    public class RedisCache<T> : IRedisCache<T> where T : class
    {
        private readonly IConfigurationHelper _configurationHelper;

        public RedisCache(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public List<T> GetList()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetListKeyName(typeof(T))];
                return list.ToList();
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetListKeyName(typeof(T))];

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
                var list = redis.Lists[GetListKeyName(typeof(T))];

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
                var list = redis.Lists[GetListKeyName(typeof(T))];
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
                var list = redis.Lists[GetListKeyName(typeof(T))];
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
                var list = redis.Lists[GetListKeyName(typeof(T))];
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
                var list = redis.Lists[GetListKeyName(typeof(T))];

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
                var list = redis.Lists[GetListKeyName(typeof(T))];

                list.Add(entity);
            }
        }

        public void Replace(Expression<Func<T, bool>> filter, T entity)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetListKeyName(typeof(T))];
                var query = list.ToList().AsQueryable().Where(filter).FirstOrDefault();

                if (query == null) return;

                list.Remove(query);
                list.Add(entity);
            }
        }
        
        public void RemoveAll()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.FlushAll();
            }
        }

        public void Remove(Expression<Func<T, bool>> filter, T entity)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var redis = client.As<T>();
                var list = redis.Lists[GetListKeyName(typeof(T))];
                var query = list.ToList().AsQueryable().Where(filter).FirstOrDefault();

                if (query == null) return;

                list.Remove(query);
            }
        }
        
        private static string GetListKeyName(Type src)
        {
            return string.Format("{0}s", src.Name.ToLower());
        }
    }
}
