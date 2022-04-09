using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.TerritoryCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.TerritoryHandlers
{
    public class DeleteTerritoryHandler : IRequestHandler<DeleteTerritoryCommand, ResponseBuilder<TerritoryResponse>>
    {
        private readonly ITerritoryRepository _territoryRepository;
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public DeleteTerritoryHandler(ITerritoryRepository territoryRepository, IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _territoryRepository = territoryRepository;
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }

        public async Task<ResponseBuilder<TerritoryResponse>> Handle(DeleteTerritoryCommand request, CancellationToken cancellationToken)
        {
            var territoryToDeleteEntity = _territoryRepository.GetById(request.Id);
            if (territoryToDeleteEntity == null)
            {
                return new ResponseBuilder<TerritoryResponse> { Message = $"Territory with id={request.Id} not found!", Data = null };
            }

            // Find employeeTerritories with this TerritoryId
            var employeeTerritoriesWithThisTerritoryId = _employeeTerritoryRepository.GetByTerritoryId(request.Id).ToList();
            if (employeeTerritoriesWithThisTerritoryId.Any())
            {
                _employeeTerritoryRepository.DeleteRange(employeeTerritoriesWithThisTerritoryId);
            }

            _territoryRepository.Delete(territoryToDeleteEntity);
            var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(territoryToDeleteEntity);
            return new ResponseBuilder<TerritoryResponse> { Message = $"Territory deleted", Data = response };
        }
    }
}
