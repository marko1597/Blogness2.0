using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class HobbyRestResource : IHobbyRestResource
    {
        public List<Hobby> GetByUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Hobby Add(Hobby hobby, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public Hobby Update(Hobby hobby, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int hobbyId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
