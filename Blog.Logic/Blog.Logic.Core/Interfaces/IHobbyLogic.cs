using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IHobbyLogic
    {
        List<Hobby> GetByUser(int userId);
        Hobby Add(Hobby hobby);
        Hobby Update(Hobby hobby);
        bool Delete(int hobbyId);
    }
}
