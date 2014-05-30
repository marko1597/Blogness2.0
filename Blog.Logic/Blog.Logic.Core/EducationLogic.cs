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

        public bool Add(Education education)
        {
            try
            {
                _educationRepository.Add(EducationMapper.ToEntity(education));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Education education)
        {
            try
            {
                 _educationRepository.Edit(EducationMapper.ToEntity(education));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int educationId)
        {
            try
            {
                _educationRepository.Delete(_educationRepository.Find(a => a.EducationId == educationId, false).FirstOrDefault());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
