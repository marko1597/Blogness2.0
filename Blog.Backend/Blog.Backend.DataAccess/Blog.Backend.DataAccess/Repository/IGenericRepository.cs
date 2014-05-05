﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Backend.DataAccess.Repository
{
    // ReSharper disable once UnusedTypeParameter
    public interface IGenericRepository<T> where T: class
    {
        #pragma warning disable 693
        IList<T> GetAll();
        IList<T> Find(Expression<Func<T, bool>> predicate, bool loadChildren);
        IList<T> Find(Expression<Func<T, bool>> predicate, bool loadChildren, int threshold);
        IList<T> Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T Add(T entity);
        T Edit(T entity);
        void Delete(T entity);
        void Save();

        #pragma warning restore 693
    }
}
