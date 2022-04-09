using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public Customer GetById(string customerId)
        {
            return NorthwndContext.Customers
                .AsNoTracking()
                .FirstOrDefault(x => x.CustomerId.Equals(customerId));
        }
    }
}
