using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IHobby
    {
        List<Hobby> GetByUser(int userId);
        Hobby Add(Hobby hobby);
        Hobby Update(Hobby hobby);
        void Delete(Hobby hobby);
    }
}
