using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IEmployeeTerritoryRepository : IRepository<EmployeeTerritory>
    {
        IEnumerable<EmployeeTerritory> GetByEmployeeId(int employeeId);
        IEnumerable<EmployeeTerritory> GetByTerritoryId(string territoryId);
        IEnumerable<EmployeeTerritory> GetByEmployeeIdAndTerritoryId(int employeeId, string territoryId);
    }
}
