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
    public class TerritoryRepository : Repository<Territory>, ITerritoryRepository
    {
        public TerritoryRepository(NorthwndContext northwndContext) : base(northwndContext)
        {
            
        }
        public override IEnumerable<Territory> GetAll()
        {
            return NorthwndContext.Territories
                .AsNoTracking()
                .Include(x => x.Region);
        }

        public override async Task<List<Territory>> GetAllAsync()
        {
            return await NorthwndContext.Territories
                .AsNoTracking()
                .Include(x => x.Region)
                .ToListAsync();
        }

        public Territory GetById(string id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Territories
                    .AsNoTracking()
                    .Include(x => x.Region)
                    .FirstOrDefault(x => x.TerritoryId.Equals(id));
            }

            return NorthwndContext.Territories
                    .AsNoTracking()
                    .FirstOrDefault(x => x.TerritoryId.Equals(id));
        }

        public async Task<Territory> GetByIdAsync(string id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Territories
                    .AsNoTracking()
                    .Include(x => x.Region)
                    .FirstOrDefaultAsync(x => x.TerritoryId.Equals(id));
            }

            return await NorthwndContext.Territories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.TerritoryId.Equals(id));
        }

        public IEnumerable<Territory> GetByRegionId(int regionId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Territories
                    .AsNoTracking()
                    .Where(x => x.RegionID == regionId)
                    .Include(x => x.Region);
            }

            return NorthwndContext.Territories
                    .AsNoTracking()
                    .Where(x => x.RegionID == regionId);
        }

        public async Task<List<Territory>> GetByRegionIdAsync(int regionId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Territories
                    .AsNoTracking()
                    .Where(x => x.RegionID == regionId)
                    .Include(x => x.Region)
                    .ToListAsync();
            }

            return await NorthwndContext.Territories
                    .AsNoTracking()
                    .Where(x => x.RegionID == regionId)
                    .ToListAsync();
        }
    }
}
