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

        public override async Task<List<Employee>> GetAllAsync()
        {
            return await NorthwndContext.Employees
                .AsNoTracking()
                .Include(x => x.ReportsToEmployee)
                .ToListAsync();
        }

        public async Task<List<Employee>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return await NorthwndContext.Employees
                .AsNoTracking()
                .Include(x => x.ReportsToEmployee)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
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

        public async Task<Employee> GetByIdAsync(int id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Employees
                    .AsNoTracking()
                    .Include(x => x.ReportsToEmployee)
                    .FirstOrDefaultAsync(x => x.EmployeeId == id);
            }

            return await NorthwndContext.Employees
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public IEnumerable<Employee> GetByReportsTo(int id)
        {
            return NorthwndContext.Employees
                .AsNoTracking()
                .Where(x => x.ReportsTo == id);
        }

        public async Task<List<Employee>> GetByReportsToAsync(int id)
        {
            return await NorthwndContext.Employees
                .AsNoTracking()
                .Where(x => x.ReportsTo == id)
                .ToListAsync();
        }
    }
}
