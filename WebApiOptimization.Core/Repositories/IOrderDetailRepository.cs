using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        IEnumerable<OrderDetail> GetByOrderId(int orderId, bool eagerLoading = false);
        IEnumerable<OrderDetail> GetByProductId(int productId, bool eagerLoading = false);
        IEnumerable<OrderDetail> GetByOrderIdAndProductId(int orderId, int productId, bool eagerLoading = false);
    }
}
