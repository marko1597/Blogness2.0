using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;
using Blog.Common.Utils;

namespace Blog.Logic.Core
{
    public class AlbumLogic : IAlbumLogic
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumLogic(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public List<Album> GetByUser(int userId)
        {
            var albums = new List<Album>();
            try
            {
                var db = _albumRepository.Find(a => a.UserId == userId, true).ToList();
                db.ForEach(a => albums.Add(AlbumMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return albums;
        }

        public Album GetUserDefaultGroup(int userId)
        {
            try
            {
                var db = _albumRepository.Find(a => a.IsUserDefault && a.UserId == userId, false).FirstOrDefault();

                if (db != null)
                {
                    return AlbumMapper.ToDto(db);
                }

                return new Album().GenerateError<Album>(
                    (int)Constants.Error.RecordNotFound,
                    string.Format("Cannot find default album for user with Id {0}", userId));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int albumId)
        {
            try
            {
                var db = _albumRepository.Find(a => a.AlbumId == albumId, false).FirstOrDefault();
                if (db == null) return false;

                _albumRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Album Add(Album album)
        {
            try
            {
                return AlbumMapper.ToDto(_albumRepository.Add(AlbumMapper.ToEntity(album)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Album Update(Album album)
        {
            try
            {
                return AlbumMapper.ToDto(_albumRepository.Edit(AlbumMapper.ToEntity(album)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
