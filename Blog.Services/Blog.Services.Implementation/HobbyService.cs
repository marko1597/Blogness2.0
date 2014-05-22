using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
{
    public class HobbyService : IHobby
    {
        public List<Hobby> GetByUser(int userId)
        {
            return HobbyFactory.GetInstance().CreateHobby().GetByUser(userId);
        }

        public bool Add(Hobby hobby)
        {
            return HobbyFactory.GetInstance().CreateHobby().Add(hobby);
        }

        public bool Update(Hobby hobby)
        {
            return HobbyFactory.GetInstance().CreateHobby().Update(hobby);
        }

        public bool Delete(int hobbyId)
        {
            return HobbyFactory.GetInstance().CreateHobby().Delete(hobbyId);
        }
    }
}
