using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IEmployeeTerritoryRepository : IRepository<EmployeeTerritory>
    {
        Task<List<EmployeeTerritory>> GetAllPagedAsync(int pageNumber, int pageSize);
        IEnumerable<EmployeeTerritory> GetByEmployeeId(int employeeId, bool eagerLoading = false);
        Task<List<EmployeeTerritory>> GetByEmployeeIdAsync(int employeeId, bool eagerLoading = false);
        IEnumerable<EmployeeTerritory> GetByTerritoryId(string territoryId, bool eagerLoading = false);
        Task<List<EmployeeTerritory>> GetByTerritoryIdAsync(string territoryId, bool eagerLoading = false);
        IEnumerable<EmployeeTerritory> GetByEmployeeIdAndTerritoryId(int employeeId, string territoryId, bool eagerLoading = false);
        Task<List<EmployeeTerritory>> GetByEmployeeIdAndTerritoryIdAsync(int employeeId, string territoryId, bool eagerLoading = false);
    }
}
