using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<List<Customer>> GetAllPagedAsync(int pageNumber, int pageSize);
        Customer GetById(string customerId);
        Task<Customer> GetByIdAsync(string customerId);  
    }
}
