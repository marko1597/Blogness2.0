using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Backend.DataAccess.BlogService.Repository
{
    public interface IRepository
    {
        TE Add<TE>(TE entity) where TE : class;
        TE Update<TE>(TE entity) where TE : class;
        bool Delete<TE>(TE entity) where TE : class;
        IQueryable<TE> Select<TE>() where TE : class;
        TE Select<TE>(object key) where TE : class;
        IList<TE> Find<TE>(Func<TE, bool> expressions) where TE : class, new();
        int SaveChanges();
        int SaveChanges(bool validateEntities);
    }
}
