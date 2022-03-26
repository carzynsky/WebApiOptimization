using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Territory;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.TerritoryHandlers
{
    public class DeleteTerritoryHandler : IRequestHandler<DeleteTerritoryCommand, TerritoryResponse>
    {
        private readonly ITerritoryRepository _territoryRepository;
        public DeleteTerritoryHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }
        public async Task<TerritoryResponse> Handle(DeleteTerritoryCommand request, CancellationToken cancellationToken)
        {
            var territoryToDeleteEntity = _territoryRepository.GetById(request.Id);
            if (territoryToDeleteEntity == null)
            {
                return null;
            }

            _territoryRepository.Delete(territoryToDeleteEntity);
            var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(territoryToDeleteEntity);
            return response;
        }
    }
}
