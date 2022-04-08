using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerCustomerDemoRepository : IRepository<CustomerCustomerDemo>
    {
        IEnumerable<CustomerCustomerDemo> GetByCustomerId(int customerId);
    }
}
