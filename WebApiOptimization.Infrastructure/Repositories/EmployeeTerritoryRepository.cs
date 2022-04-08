﻿using Microsoft.EntityFrameworkCore;
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
                .Include(x => x.Employee)
                .Include(x => x.Territory);
        }

        public IEnumerable<EmployeeTerritory> GetByEmployeeId(int employeeId)
        {
            return NorthwndContext.EmployeeTerritories
                .Where(x => x.EmployeeID == employeeId)
                .Include(x => x.Employee)
                .Include(x => x.Territory);
        }

        public IEnumerable<EmployeeTerritory> GetByTerritoryId(string territoryId)
        {
            return NorthwndContext.EmployeeTerritories
                .Where(x => x.TerritoryID == territoryId)
                .Include(x => x.Employee)
                .Include(x => x.Territory);
        }
        public IEnumerable<EmployeeTerritory> GetByEmployeeIdAndTerritoryId(int employeeId, string territoryId)
        {
            return NorthwndContext.EmployeeTerritories
                .Where(x => x.EmployeeID == employeeId && x.TerritoryID == territoryId)
                .Include(x => x.Employee)
                .Include(x => x.Territory);
        }
    }
}
