using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class MediaGroupLogic
    {
        private readonly IMediaGroupRepository _mediaGroupRepository;

        public MediaGroupLogic(IMediaGroupRepository mediaGroupRepository)
        {
            _mediaGroupRepository = mediaGroupRepository;
        }

        public List<MediaGroup> GetByUser(int userId)
        {
            var mediaGroup = new List<MediaGroup>();
            try
            {
                var db = _mediaGroupRepository.Find(a => a.UserId == userId, true).ToList();
                db.ForEach(a => mediaGroup.Add(MediaGroupMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mediaGroup;
        }

        public MediaGroup GetUserDefaultGroup(int userId)
        {
            var mediaGroup = new MediaGroup();
            try
            {
                var db = _mediaGroupRepository.Find(a => a.IsUserDefault && a.UserId == userId, false).First();
                mediaGroup = MediaGroupMapper.ToDto(db);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mediaGroup;
        }

        public bool Delete(MediaGroup mediaGroup)
        {
            try
            {
                _mediaGroupRepository.Delete(MediaGroupMapper.ToEntity(mediaGroup));
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool Add(MediaGroup mediaGroup)
        {
            try
            {
                _mediaGroupRepository.Add(MediaGroupMapper.ToEntity(mediaGroup));
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
        }

        public bool Update(MediaGroup mediaGroup)
        {
            try
            {
                _mediaGroupRepository.Edit(MediaGroupMapper.ToEntity(mediaGroup));
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
