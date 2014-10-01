using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.DataAccess.Database.Entities.Objects;
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

        private IPropertyReflection _propertyReflection;
        public IPropertyReflection PropertyReflection
        {
            get { return _propertyReflection ?? new PropertyReflection(); }
            set { _propertyReflection = value; }
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
            var userId = GetUserId(entity);
            if (userId > 0)
            {
                entity = SetCreatedDateValues(entity, userId, DateTime.Now);
                entity = SetModifiedDateValues(entity, userId);
            }
            
            _context.Set<T>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();

            return entity;
        }
        
        public virtual T Edit(T entity)
        {
            var userId = GetUserId(entity);
            if (userId > 0)
            {
                entity = SetModifiedDateValues(entity, userId);
            }

            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return entity;
        }
        
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        #region Helpers

        public T SetCreatedDateValues(T entity, int userId, DateTime createdDate)
        {
            var hasCreatedBy = PropertyReflection.HasProperty(entity, "CreatedBy");
            if (hasCreatedBy)
            {
                PropertyReflection.SetProperty(entity, "CreatedBy", userId);
            }

            var hasCreatedDate = PropertyReflection.HasProperty(entity, "CreatedDate");
            if (hasCreatedDate)
            {
                PropertyReflection.SetProperty(entity, "CreatedDate", createdDate);
            }

            return entity;
        }

        public T SetModifiedDateValues(T entity, int userId)
        {
            var hasCreatedBy = PropertyReflection.HasProperty(entity, "ModifiedBy");
            if (hasCreatedBy)
            {
                PropertyReflection.SetProperty(entity, "ModifiedBy", userId);
            }

            var hasCreatedDate = PropertyReflection.HasProperty(entity, "ModifiedDate");
            if (hasCreatedDate)
            {
                PropertyReflection.SetProperty(entity, "ModifiedDate", DateTime.Now);
            }

            return entity;
        }

        public int GetUserId(T entity)
        {
            var hasUserId = PropertyReflection.HasProperty(entity, "UserId");
            if (hasUserId) return Convert.ToInt32(PropertyReflection.GetPropertyValue(entity, "UserId"));

            var hasUserObject = PropertyReflection.HasProperty(entity, "User");
            if (!hasUserObject) return 0;

            var user = (User)PropertyReflection.GetPropertyValue(entity, "User");
            var userId = Convert.ToInt32(PropertyReflection.GetPropertyValue(user, "Id"));
            return userId;
        }

        #endregion
    }
}