using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class CustomerCustomerDemoRepository : Repository<CustomerCustomerDemo>, ICustomerCustomerDemoRepository
    {
        public CustomerCustomerDemoRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public override IEnumerable<CustomerCustomerDemo> GetAll()
        {
            return NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.CustomerType);
        }

        public IEnumerable<CustomerCustomerDemo> GetByCustomerId(string customerId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.CustomerCustomerDemos
                    .AsNoTracking()
                    .Where(x => x.CustomerID == customerId)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerType);
            }

            return NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Where(x => x.CustomerID == customerId);
        }

        public IEnumerable<CustomerCustomerDemo> GetByCustomerTypeId(string customerTypeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.CustomerCustomerDemos
                    .AsNoTracking()
                    .Where(x => x.CustomerTypeID == customerTypeId)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerType);
            }

            return NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Where(x => x.CustomerTypeID == customerTypeId);
        }

        public IEnumerable<CustomerCustomerDemo> GetByCustomerIdAndCustomerTypeId(string customerId, string customerTypeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.CustomerCustomerDemos
                    .AsNoTracking()
                    .Where(x => x.CustomerID == customerId && x.CustomerTypeID == customerTypeId)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerType);
            }

            return NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Where(x => x.CustomerID == customerId && x.CustomerTypeID == customerTypeId);
        }
    }
}
