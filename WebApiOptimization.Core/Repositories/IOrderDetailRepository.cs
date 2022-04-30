using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<OrderDetail>> GetAllPagedAsync(int pageNumber, int pageSize);
        IEnumerable<OrderDetail> GetByOrderId(int orderId, bool eagerLoading = false);
        Task<List<OrderDetail>> GetByOrderIdAsync(int orderId, bool eagerLoading = false);
        IEnumerable<OrderDetail> GetByProductId(int productId, bool eagerLoading = false);
        Task<List<OrderDetail>> GetByProductIdAsync(int productId, bool eagerLoading = false);
        IEnumerable<OrderDetail> GetByOrderIdAndProductId(int orderId, int productId, bool eagerLoading = false);
        Task<List<OrderDetail>> GetByOrderIdAndProductIdAsync(int orderId, int productId, bool eagerLoading = false);
    }
}
