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
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Supplier);
        }

        public IEnumerable<Product> GetByCategoryId(int categoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryID == categoryId)
                    .Include(x => x.Category)
                    .Include(x => x.Supplier);
            }

            return NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryID == categoryId);
        }

        public IEnumerable<Product> GetBySupplierId(int supplierId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.SupplierID == supplierId)
                    .Include(x => x.Category)
                    .Include(x => x.Supplier);
            }

            return NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.SupplierID == supplierId);
        }
    }
}
