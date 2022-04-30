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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public async Task<List<Customer>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await NorthwndContext.Customers
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Customer GetById(string customerId)
        {
            return NorthwndContext.Customers
                .AsNoTracking()
                .FirstOrDefault(x => x.CustomerID.Equals(customerId));
        }

        public async Task<Customer> GetByIdAsync(string customerId)
        {
            return await NorthwndContext.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerID.Equals(customerId));
        }
    }
}
