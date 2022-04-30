using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories.Base;

namespace WebApiOptimization.Core.Repositories
{
    public interface IShipperRepository : IRepository<Shipper>
    {
        Task<List<Shipper>> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
