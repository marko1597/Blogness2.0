using System;
using System.Data.Entity;
using System.Linq;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class MediaRepository : GenericRepository<BlogDb, Media>, IMediaRepository
    {
        public override Media Add(Media entity)
        {
            var album = Context.Albums.Where(a => a.AlbumId == entity.AlbumId).FirstOrDefault();
            if (album == null) throw new Exception(string.Format("Missing album on adding media {0}", entity.CustomName));

            entity.CreatedBy = album.UserId;
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedBy = album.UserId;
            entity.ModifiedDate = DateTime.Now;

            Context.Set<Media>().Add(entity);
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();

            return entity;
        }
    }
}
