using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Territory;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Territory
{
    public class GetTerritoryByIdHandler : IRequestHandler<GetTerritoryByIdQuery, TerritoryResponse>
    {
        private readonly ITerritoryRepository _territoryRepository;
        public GetTerritoryByIdHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }
        public async Task<TerritoryResponse> Handle(GetTerritoryByIdQuery request, CancellationToken cancellationToken)
        {
            var territoryEntity = _territoryRepository.GetById(request.Id);
            var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(territoryEntity);
            return response;
        }
    }
}
