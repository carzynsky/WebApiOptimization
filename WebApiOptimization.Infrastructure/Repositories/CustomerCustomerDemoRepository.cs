using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;
using System.Linq;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class CustomerCustomerDemoRepository : Repository<CustomerCustomerDemo>, ICustomerCustomerDemoRepository
    {
        public CustomerCustomerDemoRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public override IEnumerable<CustomerCustomerDemo> GetAll()
        {
            return NorthwndContext.CustomerCustomerDemos;
        }

        public IEnumerable<CustomerCustomerDemo> GetByCustomerId(int customerId)
        {
            return NorthwndContext.CustomerCustomerDemos
                .Where(x => x.CustomerID == customerId);
                
        }
    }
}
