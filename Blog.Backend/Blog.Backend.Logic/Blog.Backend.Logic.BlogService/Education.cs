using System;
using System.Collections.Generic;
using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService
{
    public class Education
    {
        private readonly IEducationResource _educationResource;

        public Education(IEducationResource educationResource)
        {
            _educationResource = educationResource;
        }

        public List<Services.BlogService.Contracts.BlogObjects.Education> GetByUser(int userId)
        {
            var education = new List<Services.BlogService.Contracts.BlogObjects.Education>();
            try
            {
                education = _educationResource.Get(a => a.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return education;
        }

        public Services.BlogService.Contracts.BlogObjects.Education Add(Services.BlogService.Contracts.BlogObjects.Education education)
        {
            try
            {
                return _educationResource.Add(education);
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Education();
            }
        }

        public Services.BlogService.Contracts.BlogObjects.Education Update(Services.BlogService.Contracts.BlogObjects.Education education)
        {
            try
            {
                return _educationResource.Update(education);
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Education();
            }
        }

        public void Delete(Services.BlogService.Contracts.BlogObjects.Education education)
        {
            try
            {
                _educationResource.Delete(education);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
