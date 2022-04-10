using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetById(int id, bool eagerLoading = false);
        IEnumerable<Order> GetByEmployeeId(int employeeId, bool eagerLoading = false);
        IEnumerable<Order> GetByCustomerId(string customerId, bool eagerLoading = false);
        IEnumerable<Order> GetByShipperId(int shipperId, bool eagerLoading = false);
    }
}
