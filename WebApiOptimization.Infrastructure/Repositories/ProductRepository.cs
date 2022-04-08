using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(NorthwndContext northwndContext) : base(northwndContext)
        {

        }
        public override IEnumerable<Product> GetAll()
        {
            return NorthwndContext.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier);
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId)
        {
            return NorthwndContext.Products
                .Where(x => x.CategoryID == categoryId)
                .Include(x => x.Category)
                .Include(x => x.Supplier);
        }

        public IEnumerable<Product> GetBySupplierId(int supplierId)
        {
            return NorthwndContext.Products
                .Where(x => x.SupplierID == supplierId)
                .Include(x => x.Category)
                .Include(x => x.Supplier);
        }
    }
}
