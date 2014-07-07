using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Blog.Common.Utils.Helpers.Interfaces;
using Newtonsoft.Json;
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
                var entities = client.GetAll<T>();
                return entities.ToList();
            }
        }

        public T Get(int id, string key)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var entity = client.Get<T>(string.Format("{0}:{1}", key, id));
                return entity;
            }
        }

        public T Get(string name, string key)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var entity = client.Get<T>(string.Format("{0}:{1}", key, name));
                return entity;
            }
        }

        public List<T> GetListByKey(string key)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var entities = client.GetEntry(key);
                return JsonConvert.DeserializeObject<List<T>>(entities);
            }
        }

        public void SetListByKey(string key, List<T> value)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                var entities = JsonConvert.SerializeObject(value);
                client.SetEntry(key, entities);
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

        public void SetAll(List<T> entities)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.StoreAll(entities);
            }
        }

        public void Set(T entity)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.Store(entity);
            }
        }

        public void RemoveAll()
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.FlushAll();
            }
        }

        public void Remove(int id, string key)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.Remove(string.Format("{0}:{1}", key, id));
            }
        }

        public void Remove(string name, string key)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.Remove(string.Format("{0}:{1}", key, name));
            }
        }

        public void Replace(string key, T entity)
        {
            using (var client = new RedisClient(_configurationHelper.GetAppSettings("RedisServer")))
            {
                client.Replace(key, entity);
            }
        }
    }
}
