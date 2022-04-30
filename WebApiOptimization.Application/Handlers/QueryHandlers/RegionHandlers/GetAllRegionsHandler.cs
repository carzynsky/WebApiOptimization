using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.RegionQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.RegionHandlers
{
    public class GetAllRegionsHandler : IRequestHandler<GetAllRegionsQuery, ResponseBuilder<IEnumerable<RegionResponse>>>
    {
        private readonly IRegionRepository _regionRepository; 

        public GetAllRegionsHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<RegionResponse>>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            List<Region> regions;
            List<RegionResponse> regionsDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                regions = await _regionRepository.GetAllAsync();
                regionsDto = RegionMapper.Mapper.Map<List<RegionResponse>>(regions);
                return new ResponseBuilder<IEnumerable<RegionResponse>> { Message = "OK.", Data = regionsDto };
            }

            regions = await _regionRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            regionsDto = RegionMapper.Mapper.Map<List<RegionResponse>>(regions);
            return new PagedResponse<IEnumerable<RegionResponse>>(regionsDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
