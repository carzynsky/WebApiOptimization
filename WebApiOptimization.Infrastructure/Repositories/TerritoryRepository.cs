using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
    }
}
