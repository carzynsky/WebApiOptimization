using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetByCategoryId(int categoryId);
        IEnumerable<Product> GetBySupplierId(int supplierId);
    }
}
