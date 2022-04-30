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
    public class CustomerDemographicRepository : Repository<CustomerDemographic>, ICustomerDemographicRepository
    {
        public CustomerDemographicRepository(NorthwndContext northwndContext) : base(northwndContext)
        {
            
        }

        public async Task<List<CustomerDemographic>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await NorthwndContext.CustomerDemographics
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public CustomerDemographic GetByCustomerTypeId(string customerTypeId)
        {
            return NorthwndContext.CustomerDemographics
                .FirstOrDefault(x => x.CustomerTypeId == customerTypeId);
        }

        public async Task<CustomerDemographic> GetByCustomerTypeIdAsync(string customerTypeId)
        {
            return await NorthwndContext.CustomerDemographics
                .FirstOrDefaultAsync(x => x.CustomerTypeId == customerTypeId);
        }
    }
}
