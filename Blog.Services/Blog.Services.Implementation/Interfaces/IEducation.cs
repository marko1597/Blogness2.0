using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IEducation
    {
        List<Education> GetByUser(int userId);
        bool Add(Education education);
        bool Update(Education education);
        bool Delete(int educationId);
    }
}
