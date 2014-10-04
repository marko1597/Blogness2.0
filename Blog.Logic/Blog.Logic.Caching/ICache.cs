using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Logic.Caching
{
    public interface ICache<T> where T : class
    {
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy); 
        List<T> GetListByKey(string key);
        T Get(int id, string key);
        T Get(string name, string key);
        List<string> GetKeys();
        void SetAll(List<T> entities);
        void Set(T entity);
        void SetListByKey(string key, List<T> value);
        List<T> SetItemAndUpdateList(string key, T entity, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        void RemoveAll();
        void Remove(int id, string key);
        void Remove(string name, string key);
        void Replace(string key, T entity);
    }
}
