using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.RegionQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Region
{
    public class GetRegionByIdHandler : IRequestHandler<GetRegionByIdQuery, ResponseBuilder<RegionResponse>>
    {
        private readonly IRegionRepository _regionRepository;

        public GetRegionByIdHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ResponseBuilder<RegionResponse>> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            var regionEntity = await _regionRepository.GetByIdAsync(request.Id);
            if(regionEntity == null)
            {
                return new ResponseBuilder<RegionResponse> { Message = $"Region with id={request.Id} not found!", Data = null };
            }

            var response = RegionMapper.Mapper.Map<RegionResponse>(regionEntity);
            return new ResponseBuilder<RegionResponse> { Message = "OK.", Data = response };
        }
    }
}
