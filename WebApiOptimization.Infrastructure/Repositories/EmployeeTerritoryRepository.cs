using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class EmployeeTerritoryRepository : Repository<EmployeeTerritory>, IEmployeeTerritoryRepository
    {
        public EmployeeTerritoryRepository(NorthwndContext northwndContext) : base(northwndContext)
        {
           
        }
        public override IEnumerable<EmployeeTerritory> GetAll()
        {
            return NorthwndContext.EmployeeTerritories
                .AsNoTracking()
                .Include(x => x.Employee)
                .Include(x => x.Territory);
        }

        public IEnumerable<EmployeeTerritory> GetByEmployeeId(int employeeId, bool eagerloading = false)
        {
            if (eagerloading)
            {
                return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory);
            }

            return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId);
        }

        public IEnumerable<EmployeeTerritory> GetByTerritoryId(string territoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.TerritoryID == territoryId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory);
            }

            return NorthwndContext.EmployeeTerritories
                   .AsNoTracking()
                   .Where(x => x.TerritoryID == territoryId);
        }

        public IEnumerable<EmployeeTerritory> GetByEmployeeIdAndTerritoryId(int employeeId, string territoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory);
            }

            return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId);
        }
    }
}
