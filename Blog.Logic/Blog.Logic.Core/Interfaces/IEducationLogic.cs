using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IEducationLogic
    {
        List<Education> GetByUser(int userId);
        Education Add(Education education);
        Education Update(Education education);
        bool Delete(int educationId);
    }
}
