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
        public override List<Product> GetAll()
        {
            return NorthwndContext.Products.Include(x => x.Category).Include(x => x.Supplier).ToList();
        }
    }
}
