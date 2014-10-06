using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Logic.Caching
{
    public interface ICache<T> where T : class
    {
        T GetEntry(string key);
        T GetEntry(int id);
        List<T> GetEntriesAsList(); 
        void SetEntry(T entity, string key);
        void SetEntry(T entity, int id);
        void ReplaceEntry(T entity, string key);
        void ReplaceEntry(T entity, int id);
        void RemoveEntry(string key);
        void RemoveEntry(int id);
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> filter);
        List<T> GetList(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        List<T> GetList(Expression<Func<T, bool>> filter, int threshold, int skip);
        List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int threshold, int skip);
        List<string> GetKeys();
        void SetList(List<T> entities);
        void AddToList(T entity);
        void ClearList();
        void RemoveAll();
    }
}
