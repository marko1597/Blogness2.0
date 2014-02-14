using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class HobbyService : IHobby
    {
        public List<Contracts.BlogObjects.Hobby> GetByUser(int userId)
        {
            return HobbyFactory.GetInstance().CreateHobby().GetByUser(userId);
        }

        public Contracts.BlogObjects.Hobby Add(Contracts.BlogObjects.Hobby hobby)
        {
            return HobbyFactory.GetInstance().CreateHobby().Add(hobby);
        }

        public Contracts.BlogObjects.Hobby Update(Contracts.BlogObjects.Hobby hobby)
        {
            return HobbyFactory.GetInstance().CreateHobby().Update(hobby);
        }

        public void Delete(Contracts.BlogObjects.Hobby hobby)
        {
            HobbyFactory.GetInstance().CreateHobby().Delete(hobby);
        }
    }
}
