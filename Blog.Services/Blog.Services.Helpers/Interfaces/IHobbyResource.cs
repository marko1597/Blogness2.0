using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IHobbyResource : IHobbyService
    {
    }

    public interface IHobbyRestResource
    {
        List<Hobby> GetByUser(int userId);
        Hobby Add(Hobby hobby, string authenticationToken);
        Hobby Update(Hobby hobby, string authenticationToken);
        bool Delete(int hobbyId, string authenticationToken);
    }
}
