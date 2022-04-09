using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public override IEnumerable<OrderDetail> GetAll()
        {
            return NorthwndContext.OrderDetails
                .AsNoTracking()
                .Include(x => x.Order)
                .Include(x => x.Product);
        }

        public IEnumerable<OrderDetail> GetByOrderId(int orderId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.OrderDetails
                    .AsNoTracking()
                    .Where(x => x.OrderID == orderId)
                    .Include(x => x.Order)
                    .Include(x => x.Product);
            }

            return NorthwndContext.OrderDetails
                .AsNoTracking()
                .Where(x => x.OrderID == orderId);
        }

        public IEnumerable<OrderDetail> GetByProductId(int productId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.OrderDetails
                    .AsNoTracking()
                    .Where(x => x.ProductID == productId)
                    .Include(x => x.Order)
                    .Include(x => x.Product);
            }

            return NorthwndContext.OrderDetails
                .AsNoTracking()
                .Where(x => x.ProductID == productId);
        }

        public IEnumerable<OrderDetail> GetByOrderIdAndProductId(int orderId, int productId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.OrderDetails
                    .AsNoTracking()
                    .Where(x => x.OrderID == orderId && x.ProductID == productId)
                    .Include(x => x.Order)
                    .Include(x => x.Product);
            }
            return NorthwndContext.OrderDetails
                .AsNoTracking()
                .Where(x => x.OrderID == orderId && x.ProductID == productId);
        }
    }
}
