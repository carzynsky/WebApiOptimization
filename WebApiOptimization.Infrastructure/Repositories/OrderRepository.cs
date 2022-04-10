using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public override IEnumerable<Order> GetAll()
        {
            return NorthwndContext.Orders
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(x => x.Shipper);
        }

        public IEnumerable<Order> GetByEmployeeId(int employeeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .Include(x => x.Employee)
                    .Include(x => x.Customer)
                    .Include(x => x.Shipper);
            }

            return NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId);
        }

        public IEnumerable<Order> GetByCustomerId(string customerId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.CustomerID.Equals(customerId))
                    .Include(x => x.Employee)
                    .Include(x => x.Customer)
                    .Include(x => x.Shipper);
            }

            return NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.CustomerID.Equals(customerId));
        }

        public IEnumerable<Order> GetByShipperId(int shipperId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Orders
                   .AsNoTracking()
                   .Where(x => x.ShipVia == shipperId)
                   .Include(x => x.Employee)
                   .Include(x => x.Customer)
                   .Include(x => x.Shipper);
            }

            return NorthwndContext.Orders
                   .AsNoTracking()
                   .Where(x => x.ShipVia == shipperId);
        }

        public Order GetById(int id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Orders
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .Include(x => x.Customer)
                    .Include(x => x.Shipper)
                    .FirstOrDefault(x => x.OrderID == id);
            }

            return NorthwndContext.Orders
                    .AsNoTracking()
                    .FirstOrDefault(x => x.OrderID == id);
        }
    }
}
