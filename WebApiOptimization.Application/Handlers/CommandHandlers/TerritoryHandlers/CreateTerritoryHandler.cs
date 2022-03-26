using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Territory;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.TerritoryHandlers
{
    public class CreateTerritoryHandler : IRequestHandler<CreateTerritoryCommand, TerritoryResponse>
    {
        private readonly ITerritoryRepository _territoryRepository;
        public CreateTerritoryHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }
        public async Task<TerritoryResponse> Handle(CreateTerritoryCommand request, CancellationToken cancellationToken)
        {
            var territoryEntity = TerritoryMapper.Mapper.Map<Territory>(request);
            if(territoryEntity == null)
            {
                return null;
            }

            var newTerritory = _territoryRepository.Add(territoryEntity);
            var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(newTerritory);
            return response;
        }
    }
}
