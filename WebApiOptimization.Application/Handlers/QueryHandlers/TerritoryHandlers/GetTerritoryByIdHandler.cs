using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.TerritoryQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Territory
{
    public class GetTerritoryByIdHandler : IRequestHandler<GetTerritoryByIdQuery, ResponseBuilder<TerritoryResponse>>
    {
        private readonly ITerritoryRepository _territoryRepository;

        public GetTerritoryByIdHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        public async Task<ResponseBuilder<TerritoryResponse>> Handle(GetTerritoryByIdQuery request, CancellationToken cancellationToken)
        {
            var territoryEntity = await _territoryRepository.GetByIdAsync(request.Id, true);
            if(territoryEntity == null)
            {
                return new ResponseBuilder<TerritoryResponse> { Message = $"Territory with id={request.Id} not found!", Data = null };
            }

            var response = TerritoryMapper.Mapper.Map<TerritoryResponse>(territoryEntity);
            return new ResponseBuilder<TerritoryResponse> { Message = "OK.", Data = response };
        }
    }
}
