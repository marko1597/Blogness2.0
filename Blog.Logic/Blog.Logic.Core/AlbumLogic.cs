using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.Utils;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class AlbumLogic
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
            Album album;
            try
            {
                var db = _albumRepository.Find(a => a.IsUserDefault && a.UserId == userId, false).First();
                album = AlbumMapper.ToDto(db);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return album;
        }

        public bool Delete(int albumId)
        {
            try
            {
                _albumRepository.Delete(_albumRepository.Find(a => a.AlbumId == albumId, false).FirstOrDefault());
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool Add(Album album)
        {
            try
            {
                _albumRepository.Add(AlbumMapper.ToEntity(album));
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool Update(Album album)
        {
            try
            {
                _albumRepository.Edit(AlbumMapper.ToEntity(album));
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }
    }
}
