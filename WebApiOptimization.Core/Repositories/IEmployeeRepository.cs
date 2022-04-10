using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Employee GetById(int id, bool eagerLoading = false);
        IEnumerable<Employee> GetByReportsTo(int id);
    }
}
