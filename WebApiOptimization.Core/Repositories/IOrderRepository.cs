using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order GetById(int id, bool eagerLoading = false);
        Task<Order> GetByIdAsync(int id, bool eagerLoading = false);
        IEnumerable<Order> GetByEmployeeId(int employeeId, bool eagerLoading = false);
        Task<List<Order>> GetByEmployeeIdAsync(int employeeId, bool eagerLoading = false);
        IEnumerable<Order> GetByCustomerId(string customerId, bool eagerLoading = false);
        Task<List<Order>> GetByCustomerIdAsync(string customerId, bool eagerLoading = false);
        IEnumerable<Order> GetByShipperId(int shipperId, bool eagerLoading = false);
        Task<List<Order>> GetByShipperIdAsync(int shipperId, bool eagerLoading = false);
    }
}
