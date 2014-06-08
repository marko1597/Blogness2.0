using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IHobby
    {
        List<Hobby> GetByUser(int userId);
        Hobby Add(Hobby hobby);
        Hobby Update(Hobby hobby);
        bool Delete(int hobbyId);
    }
}
