using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async override Task<List<Order>> GetAllAsync()
        {
            return await NorthwndContext.Orders
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(x => x.Shipper)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await NorthwndContext.Orders
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Customer)
                .Include(x => x.Shipper)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

        public async Task<List<Order>> GetByEmployeeIdAsync(int employeeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .Include(x => x.Employee)
                    .Include(x => x.Customer)
                    .Include(x => x.Shipper)
                    .ToListAsync();
            }

            return await NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .ToListAsync();
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

        public async Task<List<Order>> GetByCustomerIdAsync(string customerId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.CustomerID.Equals(customerId))
                    .Include(x => x.Employee)
                    .Include(x => x.Customer)
                    .Include(x => x.Shipper)
                    .ToListAsync();
            }

            return await NorthwndContext.Orders
                    .AsNoTracking()
                    .Where(x => x.CustomerID.Equals(customerId))
                    .ToListAsync();
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

        public async Task<List<Order>> GetByShipperIdAsync(int shipperId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Orders
                   .AsNoTracking()
                   .Where(x => x.ShipVia == shipperId)
                   .Include(x => x.Employee)
                   .Include(x => x.Customer)
                   .Include(x => x.Shipper)
                   .ToListAsync();
            }

            return await NorthwndContext.Orders
                   .AsNoTracking()
                   .Where(x => x.ShipVia == shipperId)
                   .ToListAsync();
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

        public async Task<Order> GetByIdAsync(int id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Orders
                    .AsNoTracking()
                    .Include(x => x.Employee)
                    .Include(x => x.Customer)
                    .Include(x => x.Shipper)
                    .FirstOrDefaultAsync(x => x.OrderID == id);
            }

            return await NorthwndContext.Orders
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.OrderID == id);
        }
    }
}
