using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.Utils;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class HobbyLogic
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

        public bool Add(Hobby hobby)
        {
            try
            {
                _hobbyRepository.Add(HobbyMapper.ToEntity(hobby));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Hobby hobby)
        {
            try
            {
                _hobbyRepository.Edit(HobbyMapper.ToEntity(hobby));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int hobbyId)
        {
            try
            {
                _hobbyRepository.Delete(_hobbyRepository.Find(a => a.HobbyId == hobbyId, false).FirstOrDefault());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
