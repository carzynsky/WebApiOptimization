using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }

        public override IEnumerable<Employee> GetAll()
        {
            return NorthwndContext.Employees
                .AsNoTracking()
                .Include(x => x.ReportsToEmployee);
        }

        public Employee GetById(int id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Employees
                    .AsNoTracking()
                    .Include(x => x.ReportsToEmployee)
                    .FirstOrDefault(x => x.EmployeeId == id);
            }

            return NorthwndContext.Employees
                    .AsNoTracking()
                    .FirstOrDefault(x => x.EmployeeId == id);
        }

        public IEnumerable<Employee> GetByReportsTo(int id)
        {
            return NorthwndContext.Employees
                .AsNoTracking()
                .Where(x => x.ReportsTo == id);
        }
    }
}
