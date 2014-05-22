using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation
{
    public interface IHobby
    {
        List<Hobby> GetByUser(int userId);
        bool Add(Hobby hobby);
        bool Update(Hobby hobby);
        bool Delete(int hobbyId);
    }
}
