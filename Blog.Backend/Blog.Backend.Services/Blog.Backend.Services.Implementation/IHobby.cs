using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IHobby
    {
        List<Hobby> GetByUser(int userId);
        bool Add(Hobby hobby);
        bool Update(Hobby hobby);
        bool Delete(Hobby hobby);
    }
}
