using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.RegionCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.RegionHandlers
{
    public class UpdateRegionHandler : IRequestHandler<UpdateRegionCommand, ResponseBuilder<RegionResponse>>
    {
        private readonly IRegionRepository _regionRepository;

        public UpdateRegionHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ResponseBuilder<RegionResponse>> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            var regionToUpdate = _regionRepository.GetById(request.RegionId);
            if (regionToUpdate == null)
            {
                return new ResponseBuilder<RegionResponse> { Message = $"Region with id={request.RegionId} not found!", Data = null };
            }

            var regionToUpdateEntity = RegionMapper.Mapper.Map<Region>(request);
            _regionRepository.Update(regionToUpdateEntity);
            var response = RegionMapper.Mapper.Map<RegionResponse>(regionToUpdateEntity);
            return new ResponseBuilder<RegionResponse> { Message = "Region updated.", Data = response };
        }
    }
}
