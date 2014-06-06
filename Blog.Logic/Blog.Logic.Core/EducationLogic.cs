using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class EducationLogic
    {
        private readonly IEducationRepository _educationRepository;

        public EducationLogic(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public List<Education> GetByUser(int userId)
        {
            var education = new List<Education>();
            try
            {
                var db = _educationRepository.Find(a => a.UserId == userId, true).ToList();
                db.ForEach(a => education.Add(EducationMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return education;
        }

        public Education Add(Education education)
        {
            try
            {
                return EducationMapper.ToDto(_educationRepository.Add(EducationMapper.ToEntity(education)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Education Update(Education education)
        {
            try
            {
                return EducationMapper.ToDto(_educationRepository.Edit(EducationMapper.ToEntity(education)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int educationId)
        {
            try
            {
                var db = _educationRepository.Find(a => a.EducationId == educationId, false).FirstOrDefault();
                if (db == null) return false;

                _educationRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
