using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override async Task<List<Product>> GetAllAsync()
        {
            return await NorthwndContext.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .ToListAsync();
        }

        public Product GetById(int id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return NorthwndContext.Products
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .FirstOrDefault(x => x.ProductID == id);
            }

            return NorthwndContext.Products
                    .AsNoTracking()
                    .FirstOrDefault(x => x.ProductID == id);
        }

        public async Task<Product> GetByIdAsync(int id, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Products
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .FirstOrDefaultAsync(x => x.ProductID == id);
            }

            return await NorthwndContext.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProductID == id);
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

        public async Task<List<Product>> GetByCategoryIdAsync(int categoryId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryID == categoryId)
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .ToListAsync();
            }

            return await NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryID == categoryId)
                    .ToListAsync();
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

        public async Task<List<Product>> GetBySupplierIdAsync(int supplierId, bool eagerLoading = false)
        {
            if (eagerLoading)
            {
                return await NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.SupplierID == supplierId)
                    .Include(x => x.Category)
                    .Include(x => x.Supplier)
                    .ToListAsync();
            }

            return await NorthwndContext.Products
                    .AsNoTracking()
                    .Where(x => x.SupplierID == supplierId)
                    .ToListAsync();
        }
    }
}
