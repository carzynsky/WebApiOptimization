using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetByEmployeeId(int employeeId);
        IEnumerable<Order> GetByCustomerId(string customerId);
    }
}
