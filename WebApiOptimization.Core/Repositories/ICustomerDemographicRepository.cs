using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerDemographicRepository : IRepository<CustomerDemographic>
    {
        Task<List<CustomerDemographic>> GetAllPagedAsync(int pageNumber, int pageSize);
        CustomerDemographic GetByCustomerTypeId(string customerTypeId);
        Task<CustomerDemographic> GetByCustomerTypeIdAsync(string customerTypeId);
    }
}
