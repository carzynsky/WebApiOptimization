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
    public class UpdateTerritoryHandler : IRequestHandler<UpdateTerritoryCommand, TerritoryResponse>
    {
        private readonly ITerritoryRepository _territoryRepository;
        public UpdateTerritoryHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }
        public async Task<TerritoryResponse> Handle(UpdateTerritoryCommand request, CancellationToken cancellationToken)
        {
            var territoryToUpdate = _territoryRepository.GetById(request.TerritoryId);
            if (territoryToUpdate == null)
            {
                return null;
            }

            var territoryToUpdateEntity = TerritoryMapper.Mapper.Map<Territory>(request);
            if (territoryToUpdateEntity == null)
            {
                return null;
            }

            _territoryRepository.Update(territoryToUpdateEntity);
            var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(territoryToUpdateEntity);
            return response;
        }
    }
}
