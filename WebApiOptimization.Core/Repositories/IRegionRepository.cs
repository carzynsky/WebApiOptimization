using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IRegionRepository : IRepository<Region>
    {
        Task<List<Region>> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
