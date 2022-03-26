using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Region;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Region
{
    public class GetRegionByIdHandler : IRequestHandler<GetRegionByIdQuery, RegionResponse>
    {
        private readonly IRegionRepository _regionRepository;
        public GetRegionByIdHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<RegionResponse> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            var regionEntity = _regionRepository.GetById(request.Id);
            var response = RegionMapper.Mapper.Map<RegionResponse>(regionEntity);
            return response;
        }
    }
}
