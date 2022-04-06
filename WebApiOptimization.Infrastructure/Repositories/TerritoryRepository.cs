using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            return NorthwndContext.Territories.Include(x => x.Region);
        }
    }
}
