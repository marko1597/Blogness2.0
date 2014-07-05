using System.Collections.Generic;

namespace Blog.Logic.Caching
{
    public interface ICache<T> where T : class
    {
        List<T> GetAll();
        T Get(int id, string key);
        T Get(string name, string key);
        List<string> GetKeys();
        void SetAll(List<T> entities);
        void Set(T entity);
        void RemoveAll();
        void Remove(int id, string key);
        void Remove(string name, string key);
        void Replace(string key, T entity);
    }
}
