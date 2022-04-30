using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

        public override async Task<List<CustomerCustomerDemo>> GetAllAsync()
        {
            return await NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.CustomerType)
                .ToListAsync();
        }

        public async Task<List<CustomerCustomerDemo>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.CustomerType)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

        public async Task<List<CustomerCustomerDemo>> GetByCustomerIdAsync(string customerId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.CustomerCustomerDemos
                    .AsNoTracking()
                    .Where(x => x.CustomerID == customerId)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerType)
                    .ToListAsync();
            }

            return await NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Where(x => x.CustomerID == customerId)
                .ToListAsync();
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

        public async Task<List<CustomerCustomerDemo>> GetByCustomerTypeIdAsync(string customerTypeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.CustomerCustomerDemos
                    .AsNoTracking()
                    .Where(x => x.CustomerTypeID == customerTypeId)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerType)
                    .ToListAsync();
            }

            return await NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Where(x => x.CustomerTypeID == customerTypeId)
                .ToListAsync();
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

        public async Task<List<CustomerCustomerDemo>> GetByCustomerIdAndCustomerTypeIdAsync(string customerId, string customerTypeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.CustomerCustomerDemos
                    .AsNoTracking()
                    .Where(x => x.CustomerID == customerId && x.CustomerTypeID == customerTypeId)
                    .Include(x => x.Customer)
                    .Include(x => x.CustomerType)
                    .ToListAsync();
            }

            return await NorthwndContext.CustomerCustomerDemos
                .AsNoTracking()
                .Where(x => x.CustomerID == customerId && x.CustomerTypeID == customerTypeId)
                .ToListAsync();
        }
    }
}
