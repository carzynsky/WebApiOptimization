using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
