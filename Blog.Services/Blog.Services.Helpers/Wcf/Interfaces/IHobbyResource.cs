using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IHobbyResource : IBaseResource
    {
        List<Hobby> GetByUser(int userId);
        Hobby Add(Hobby hobby);
        Hobby Update(Hobby hobby);
        bool Delete(int hobbyId);
    }
}
