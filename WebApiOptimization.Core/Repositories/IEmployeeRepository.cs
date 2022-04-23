using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetById(int id, bool eagerLoading = false);
        Task<Employee> GetByIdAsync(int id, bool eagerLoading = false);
        IEnumerable<Employee> GetByReportsTo(int id);
        Task<List<Employee>> GetByReportsToAsync(int id);
    }
}
