using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class HobbyService : IHobby
    {
        public List<Hobby> GetByUser(int userId)
        {
            return HobbyFactory.GetInstance().CreateHobby().GetByUser(userId);
        }

        public Hobby Add(Hobby hobby)
        {
            return HobbyFactory.GetInstance().CreateHobby().Add(hobby);
        }

        public Hobby Update(Hobby hobby)
        {
            return HobbyFactory.GetInstance().CreateHobby().Update(hobby);
        }

        public bool Delete(int hobbyId)
        {
            return HobbyFactory.GetInstance().CreateHobby().Delete(hobbyId);
        }
    }
}
