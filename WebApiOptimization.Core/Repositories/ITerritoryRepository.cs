using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ITerritoryRepository : IRepository<Territory>
    {
        Territory GetById(string id, bool eagerLoading = false);
        Task<Territory> GetByIdAsync(string id, bool eagerLoading = false);
        IEnumerable<Territory> GetByRegionId(int regionId, bool eagerLoading = false);
        Task<List<Territory>> GetByRegionIdAsync(int regionId, bool eagerLoading = false);
    }
}
