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
                .Include(x => x.Employee)
                .Include(x => x.Customer);
        }

        public IEnumerable<Order> GetByEmployeeId(int employeeId)
        {
            return NorthwndContext.Orders
                .Where(x => x.EmployeeID == employeeId)
                .Include(x => x.Employee)
                .Include(x => x.Customer);
        }

        public IEnumerable<Order> GetByCustomerId(string customerId)
        {
            return NorthwndContext.Orders
                .Where(x => x.CustomerID.Equals(customerId))
                .Include(x => x.Employee)
                .Include(x => x.Customer);
        }
    }
}
