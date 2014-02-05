using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Blog.Backend.DataAccess.BlogService.Repository
{
    public class Repository<T> : IRepository, IDisposable where T : DbContext
    {
        #region Fields

        private T _context;

        #endregion

        #region Properties

        public T Context
        {
            get { return _context ?? (_context = Activator.CreateInstance<T>()); }
        }

        #endregion

        #region Methods

        public TE Add<TE>(TE entity) where TE : class
        {
            try
            {
                Context.Set<TE>().Add(entity);
                Context.Entry(entity).State = EntityState.Added;
                Context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
            
        }

        public TE Update<TE>(TE entity) where TE : class
        {
            try
            {
                Context.Set<TE>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                Context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
            
        }

        public bool Delete<TE>(TE entity) where TE : class
        {
            try
            {
                Context.Set<TE>().Attach(entity);
                Context.Entry(entity).State = EntityState.Deleted;
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public int SaveChanges(bool validateEntities)
        {
            Context.Configuration.ValidateOnSaveEnabled = validateEntities;
            return Context.SaveChanges();
        }

        public IQueryable<TE> Select<TE>() where TE : class
        {
            Context.Configuration.ProxyCreationEnabled = false;
            return Context.Set<TE>();
        }

        public TE Select<TE>(object key) where TE : class
        {
            Context.Configuration.ProxyCreationEnabled = false;
            return Context.Set<TE>().Find(key);
        }

        public IList<TE> Find<TE>(Func<TE, bool> expression) where TE : class, new()
        {
            Context.Configuration.ProxyCreationEnabled = false;
            return Context.Set<TE>().Where(expression).ToList();
        }

        public IList<TE> Find<TE>(Func<TE, bool> expression, int threshold) where TE : class, new()
        {
            Context.Configuration.ProxyCreationEnabled = false;
            return Context.Set<TE>().Where(expression).Take(threshold).ToList();
        }

        #endregion

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}