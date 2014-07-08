using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class HobbyLogic : IHobbyLogic
    {
        private readonly IHobbyRepository _hobbyRepository;

        public HobbyLogic(IHobbyRepository hobbyRepository)
        {
            _hobbyRepository = hobbyRepository;
        }

        public List<Hobby> GetByUser(int userId)
        {
            var hobbies = new List<Hobby>();
            try
            {
                var db = _hobbyRepository.Find(a => a.UserId == userId, false).ToList();
                db.ForEach(a => hobbies.Add(HobbyMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return hobbies;
        }

        public Hobby Add(Hobby hobby)
        {
            try
            {
                return HobbyMapper.ToDto(_hobbyRepository.Add(HobbyMapper.ToEntity(hobby)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Hobby Update(Hobby hobby)
        {
            try
            {
                return HobbyMapper.ToDto(_hobbyRepository.Edit(HobbyMapper.ToEntity(hobby)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int hobbyId)
        {
            try
            {
                var db = _hobbyRepository.Find(a => a.HobbyId == hobbyId, false).FirstOrDefault();
                if (db == null) return false;

                _hobbyRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
