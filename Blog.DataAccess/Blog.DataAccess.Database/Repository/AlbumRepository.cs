using System;
using System.Data.Entity;
using System.Linq;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class AlbumRepository : GenericRepository<BlogDb, Album>, IAlbumRepository
    {
        public override Album Edit(Album entity)
        {
            var db = Context.Albums.FirstOrDefault(a => a.AlbumId == entity.AlbumId);
            if (db == null) throw new Exception(string.Format("Failed to fetch album with Id {0}", entity.AlbumId));

            db.AlbumName = entity.AlbumName;
            db.IsUserDefault = entity.IsUserDefault;
            db.ModifiedBy = entity.UserId;
            db.ModifiedDate = DateTime.Now;

            Context.Entry(db).State = EntityState.Modified;
            Context.SaveChanges();

            return entity;
        }
    }
}
