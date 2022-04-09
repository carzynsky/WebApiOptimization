using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.RegionCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.RegionHandlers
{
    public class DeleteRegionHandler : IRequestHandler<DeleteRegionCommand, ResponseBuilder<RegionResponse>>
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ITerritoryRepository _territoryRepository;

        public DeleteRegionHandler(IRegionRepository regionRepository, ITerritoryRepository territoryRepository)
        {
            _regionRepository = regionRepository;
            _territoryRepository = territoryRepository;
        }

        public async Task<ResponseBuilder<RegionResponse>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var regionToDelete = _regionRepository.GetById(request.Id);
            if(regionToDelete == null)
            {
                return new ResponseBuilder<RegionResponse> { Message = $"Region with id={request.Id} not found!", Data = null };
            }

            // Find territories with this regionId
            var territoriesWithThisRegionId = _territoryRepository.GetByRegionId(request.Id).ToList();
            if (territoriesWithThisRegionId.Any())
            {
                _territoryRepository.DeleteRange(territoriesWithThisRegionId);
            }

            _regionRepository.Delete(regionToDelete);
            var response = RegionMapper.Mapper.Map<RegionResponse>(regionToDelete);
            return new ResponseBuilder<RegionResponse> { Message = "Region deleted!=.", Data = response };
        }
    }
}
