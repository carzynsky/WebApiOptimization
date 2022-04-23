using Microsoft.EntityFrameworkCore;
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
