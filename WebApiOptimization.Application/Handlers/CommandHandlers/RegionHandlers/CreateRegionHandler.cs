using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Region;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.RegionHandlers
{
    public class CreateRegionHandler : IRequestHandler<CreateRegionCommand, RegionResponse>
    {
        private readonly IRegionRepository _regionRepository;
        public CreateRegionHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<RegionResponse> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var regionEntity = RegionMapper.Mapper.Map<Region>(request);
            if(regionEntity == null)
            {
                return null;
            }

            var newRegion = _regionRepository.Add(regionEntity);
            var response = RegionMapper.Mapper.Map<RegionResponse>(newRegion);
            return response;
        }
    }
}
