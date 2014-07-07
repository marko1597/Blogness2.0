using System.Collections.Generic;

namespace Blog.Logic.Caching.DataSource
{
    public interface ICacheDataSource<T> where T : class
    {
        List<T> GetList();
        List<T> GetListByKey(string key);
        T Get(int id, string key);
        T Get(string name, string key);
        List<string> GetKeys();
        void SetAll(List<T> entities);
        void Set(T entity);
        void SetListByKey(string key, List<T> value);
        void RemoveAll();
        void Remove(int id, string key);
        void Remove(string name, string key);
        void Replace(string key, T entity);
    }
}
