using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Blog.Backend.Services.BlogService.Contracts.Helper.Attribute;

namespace Blog.Backend.DataAccess.BlogService.Repository
{
    public class PropertyIncluder<TEntity> where TEntity : class
    {
        private readonly Func<DbQuery<TEntity>, DbQuery<TEntity>>  _includeMethod;
        private readonly HashSet<Type> _visitedTypes;

        public PropertyIncluder()
        {
            //Recursively get properties to include
            _visitedTypes = new HashSet<Type>();
            var propsToLoad = GetPropsToLoad(typeof (TEntity)).ToArray();
            _includeMethod = d => propsToLoad.Aggregate(d, (current, prop) => current.Include(prop));
        }

        private IEnumerable<string> GetPropsToLoad(Type type)
        {
            _visitedTypes.Add(type);

            var propsToLoad = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttributes(typeof (Include), true).Any());

            foreach (var prop in propsToLoad)
            {
                yield return prop.Name;
                if (_visitedTypes.Contains(prop.PropertyType))
                continue;
                foreach (var subProp in GetPropsToLoad(prop.PropertyType))
                yield return prop.Name + "." + subProp;
            }
        }

        public DbQuery<TEntity> BuildQuery(DbSet<TEntity> dbSet)
        {
            return _includeMethod(dbSet);
        }
    }
}

