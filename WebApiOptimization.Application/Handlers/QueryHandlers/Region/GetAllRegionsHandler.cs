using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Region;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Region
{
    public class GetAllRegionsHandler : IRequestHandler<GetAllRegionsQuery, IEnumerable<RegionResponse>>
    {
        private readonly IRegionRepository _regionRepository; 
        public GetAllRegionsHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<IEnumerable<RegionResponse>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions = _regionRepository.GetAll();
            var response = RegionMapper.Mapper.Map<IEnumerable<RegionResponse>>(regions);
            return response;
        }
    }
}
