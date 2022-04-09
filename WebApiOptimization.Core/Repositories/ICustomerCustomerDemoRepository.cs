using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerCustomerDemoRepository : IRepository<CustomerCustomerDemo>
    {
        IEnumerable<CustomerCustomerDemo> GetByCustomerId(string customerId, bool eagerLoading = false);
        IEnumerable<CustomerCustomerDemo> GetByCustomerTypeId(string customerTypeId, bool eagerLoading = false);
        IEnumerable<CustomerCustomerDemo> GetByCustomerIdAndCustomerTypeId(string customerId, string customerTypeId, bool eagerLoading = false);
    }
}
