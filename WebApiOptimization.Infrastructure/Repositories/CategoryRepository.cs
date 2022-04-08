using Microsoft.EntityFrameworkCore;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;
using WebApiOptimization.Infrastructure.Data;
using WebApiOptimization.Infrastructure.Repositories.Base;
using System.Linq;

namespace WebApiOptimization.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(NorthwndContext northwndContext) : base(northwndContext) 
        { 

        }
    }
}
