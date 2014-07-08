using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IEducationService : IBaseService
    {
        [OperationContract]
        List<Education> GetByUser(int userId);

        [OperationContract]
        Education Add(Education education);

        [OperationContract]
        Education Update(Education education);

        [OperationContract]
        bool Delete(int educationId);
    }
}
