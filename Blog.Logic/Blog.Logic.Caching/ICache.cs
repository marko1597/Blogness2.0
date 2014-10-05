using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Logic.Caching
{
    public interface ICache<T> where T : class
    {
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> filter);
        List<T> GetList(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        List<T> GetList(Expression<Func<T, bool>> filter, int threshold, int skip);
        List<T> GetList(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int threshold, int skip);
        List<string> GetKeys();
        void SetList(List<T> entities);
        void AddToList(T entity);
        void Replace(Expression<Func<T, bool>> filter, T entity);
        void RemoveAll();
        void Remove(Expression<Func<T, bool>> filter, T entity);
    }
}
