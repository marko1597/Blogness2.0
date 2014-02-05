using System;
using System.Collections.Generic;
using Blog.Backend.ResourceAccess.BlogService.Resources;

namespace Blog.Backend.Logic.BlogService
{
    public class Hobby
    {
        private readonly IHobbyResource _hobbyResource;

        public Hobby(IHobbyResource hobbyResource)
        {
            _hobbyResource = hobbyResource;
        }

        public List<Services.BlogService.Contracts.BlogObjects.Hobby> GetByUser(int userId)
        {
            var hobbies = new List<Services.BlogService.Contracts.BlogObjects.Hobby>();
            try
            {
                hobbies = _hobbyResource.Get(a => a.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return hobbies;
        }

        public Services.BlogService.Contracts.BlogObjects.Hobby Add(Services.BlogService.Contracts.BlogObjects.Hobby hobby)
        {
            try
            {
                return _hobbyResource.Add(hobby);
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Hobby();
            }
        }

        public Services.BlogService.Contracts.BlogObjects.Hobby Update(Services.BlogService.Contracts.BlogObjects.Hobby hobby)
        {
            try
            {
                return _hobbyResource.Update(hobby);
            }
            catch (Exception)
            {
                return new Services.BlogService.Contracts.BlogObjects.Hobby();
            }
        }

        public void Delete(Services.BlogService.Contracts.BlogObjects.Hobby hobby)
        {
            try
            {
                _hobbyResource.Delete(hobby);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
