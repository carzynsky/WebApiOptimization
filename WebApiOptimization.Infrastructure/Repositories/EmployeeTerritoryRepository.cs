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
                .Include(x => x.Territory.Region);
        }

        public override async Task<List<EmployeeTerritory>> GetAllAsync()
        {
            return await NorthwndContext.EmployeeTerritories
               .AsNoTracking()
               .Include(x => x.Employee)
               .Include(x => x.Territory.Region)
               .ToListAsync();
        }

        public IEnumerable<EmployeeTerritory> GetByEmployeeId(int employeeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory.Region);
            }

            return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId);
        }

        public async Task<List<EmployeeTerritory>> GetByEmployeeIdAsync(int employeeId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory.Region)
                    .ToListAsync();
            }

            return await NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId)
                    .ToListAsync();
        }

        public IEnumerable<EmployeeTerritory> GetByTerritoryId(string territoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.TerritoryID == territoryId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory.Region);
            }

            return NorthwndContext.EmployeeTerritories
                   .AsNoTracking()
                   .Where(x => x.TerritoryID == territoryId);
        }
        public async Task<List<EmployeeTerritory>> GetByTerritoryIdAsync(string territoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.TerritoryID == territoryId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory.Region)
                    .ToListAsync();
            }

            return await NorthwndContext.EmployeeTerritories
                   .AsNoTracking()
                   .Where(x => x.TerritoryID == territoryId)
                   .ToListAsync();
        }

        public IEnumerable<EmployeeTerritory> GetByEmployeeIdAndTerritoryId(int employeeId, string territoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory.Region);
            }

            return NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId);
        }


        public async Task<List<EmployeeTerritory>> GetByEmployeeIdAndTerritoryIdAsync(int employeeId, string territoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId)
                    .Include(x => x.Employee)
                    .Include(x => x.Territory.Region)
                    .ToListAsync();
            }

            return await NorthwndContext.EmployeeTerritories
                    .AsNoTracking()
                    .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId)
                    .ToListAsync();
        }
    }
}
