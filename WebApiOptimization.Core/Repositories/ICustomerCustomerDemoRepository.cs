using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerCustomerDemoRepository : IRepository<CustomerCustomerDemo>
    {
        Task<List<CustomerCustomerDemo>> GetAllPagedAsync(int pageNumber, int pageSize);
        IEnumerable<CustomerCustomerDemo> GetByCustomerId(string customerId, bool eagerLoading = false);
        Task<List<CustomerCustomerDemo>> GetByCustomerIdAsync(string customerId, bool eagerLoading = false);
        IEnumerable<CustomerCustomerDemo> GetByCustomerTypeId(string customerTypeId, bool eagerLoading = false);
        Task<List<CustomerCustomerDemo>> GetByCustomerTypeIdAsync(string customerTypeId, bool eagerLoading = false);
        IEnumerable<CustomerCustomerDemo> GetByCustomerIdAndCustomerTypeId(string customerId, string customerTypeId, bool eagerLoading = false);
        Task <List<CustomerCustomerDemo>> GetByCustomerIdAndCustomerTypeIdAsync(string customerId, string customerTypeId, bool eagerLoading = false);
    }
}
