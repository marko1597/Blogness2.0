using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class HobbyMock : IHobby
    {
        public List<Hobby> GetByUser(int userId)
        {
            var hobbies = DataStorage.Hobbies.FindAll(a => a.UserId == userId);
            return hobbies;
        }

        public bool Add(Hobby hobby)
        {
            var id = DataStorage.Hobbies.Select(a => a.HobbyId).Max();
            hobby.HobbyId = id + 1;
            DataStorage.Hobbies.Add(hobby);

            return true;
        }

        public bool Update(Hobby hobby)
        {
            var tHobby = DataStorage.Hobbies.FirstOrDefault(a => a.HobbyId == hobby.HobbyId);
            DataStorage.Hobbies.Remove(tHobby);
            DataStorage.Hobbies.Add(hobby);

            return true;
        }

        public bool Delete(int hobbyId)
        {
            var tHobby = DataStorage.Hobbies.FirstOrDefault(a => a.HobbyId == hobbyId);
            DataStorage.Hobbies.Remove(tHobby);

            return true;
        }
    }
}
