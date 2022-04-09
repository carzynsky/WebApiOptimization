using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetById(string customerId);
    }
}
