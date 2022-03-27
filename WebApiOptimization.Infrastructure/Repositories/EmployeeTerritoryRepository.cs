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

        public IEnumerable<EmployeeTerritory> GetByEmployeeId(int employeeId)
        {
            return NorthwndContext.EmployeeTerritories.Where(x => x.EmployeeId == employeeId);
        }

        public IEnumerable<EmployeeTerritory> GetByTerritoryId(string territoryId)
        {
            return NorthwndContext.EmployeeTerritories.Where(x => x.TerritoryId == territoryId);
        }
        public IEnumerable<EmployeeTerritory> GetByEmployeeIdAndTerritoryId(int employeeId, string territoryId)
        {
            return NorthwndContext.EmployeeTerritories.Where(x => x.EmployeeId == employeeId && x.TerritoryId == territoryId);
        }
    }
}
