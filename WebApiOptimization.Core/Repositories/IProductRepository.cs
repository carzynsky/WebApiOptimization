using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllPagedAsync(int pageNumber, int pageSize);
        Product GetById(int id, bool eagerLoading = false); 
        Task<Product> GetByIdAsync(int id, bool eagerLoading = false);
        IEnumerable<Product> GetByCategoryId(int categoryId, bool eagerLoading = false);
        Task<List<Product>> GetByCategoryIdAsync(int categoryId, bool eagerLoading = false);
        IEnumerable<Product> GetBySupplierId(int supplierId, bool eagerLoading = false);
        Task<List<Product>> GetBySupplierIdAsync(int supplierId, bool eagerLoading = false);
    }
}
