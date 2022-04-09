using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.TerritoryQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Territory
{
    public class GetAllTerritoriesHandler : IRequestHandler<GetAllTerritoriesQuery, ResponseBuilder<IEnumerable<TerritoryResponse>>>
    {
        private readonly ITerritoryRepository _territoryRepository;

        public GetAllTerritoriesHandler(ITerritoryRepository territoryRepository)
        {
            _territoryRepository = territoryRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<TerritoryResponse>>> Handle(GetAllTerritoriesQuery request, CancellationToken cancellationToken)
        {
            var territories = _territoryRepository.GetAll();
            var response = TerritoryMapper.Mapper.Map<IEnumerable<TerritoryResponse>>(territories);
            return new ResponseBuilder<IEnumerable<TerritoryResponse>> { Message = "OK.", Data = response };
        }
    }
}
