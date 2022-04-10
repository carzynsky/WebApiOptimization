using System.Collections.Generic;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetById(int id, bool eagerLoading = false);
        IEnumerable<Product> GetByCategoryId(int categoryId, bool eagerLoading = false);
        IEnumerable<Product> GetBySupplierId(int supplierId, bool eagerLoading = false);
    }
}
