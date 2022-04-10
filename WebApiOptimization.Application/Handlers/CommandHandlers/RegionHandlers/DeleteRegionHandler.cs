using MediatR;
using System;
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
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public DeleteRegionHandler(IRegionRepository regionRepository, ITerritoryRepository territoryRepository, IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _regionRepository = regionRepository;
            _territoryRepository = territoryRepository;
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }

        public async Task<ResponseBuilder<RegionResponse>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var regionToDelete = _regionRepository.GetById(request.Id);
            if(regionToDelete == null)
            {
                return new ResponseBuilder<RegionResponse> { Message = $"Region with id={request.Id} not found!", Data = null };
            }

            try
            {
                // Find territories with this regionId
                var territoriesWithThisRegionId = _territoryRepository.GetByRegionId(request.Id).ToList();
                if (territoriesWithThisRegionId.Any())
                {
                    foreach (var territory in territoriesWithThisRegionId)
                    {
                        var employeeTerritoryWithThisTerritory = _employeeTerritoryRepository.GetByTerritoryId(territory.TerritoryId).ToList();
                        if (employeeTerritoryWithThisTerritory.Any())
                        {
                            _employeeTerritoryRepository.DeleteRange(employeeTerritoryWithThisTerritory);
                        }
                    }

                    _territoryRepository.DeleteRange(territoriesWithThisRegionId);
                }

                _regionRepository.Delete(regionToDelete);
                var response = RegionMapper.Mapper.Map<RegionResponse>(regionToDelete);
                return new ResponseBuilder<RegionResponse> { Message = "Region deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<RegionResponse> { Message = $"Region not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
