using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    [ExcludeFromCodeCoverage]
    public abstract class GenericRepository<TC, T> : IGenericRepository<T>
        where T : class
        where TC : DbContext, new()
    {
        private TC _context = new TC();

        public TC Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public virtual IList<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.ToList();
        }

        public IList<T> Find(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            _context.Configuration.ProxyCreationEnabled = false;

            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public IList<T> Find(Expression<Func<T, bool>> predicate, bool loadChildren)
        {
            _context.Configuration.ProxyCreationEnabled = loadChildren;
            return _context.Set<T>().Where(predicate).ToList();
        }

        public IList<T> Find(Expression<Func<T, bool>> predicate, bool loadChildren, int threshold)
        {
            _context.Configuration.ProxyCreationEnabled = loadChildren;
            return _context.Set<T>().Where(predicate).Take(threshold).ToList();
        }

        public virtual T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();

            return entity;
        }
        
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public virtual T Edit(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
    }
}