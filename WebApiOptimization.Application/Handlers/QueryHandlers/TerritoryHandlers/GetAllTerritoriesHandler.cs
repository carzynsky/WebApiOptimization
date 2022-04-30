using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.TerritoryQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.TerritoryHandlers
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
            List<Territory> territories;
            List<TerritoryResponse> territoriesDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                territories = await _territoryRepository.GetAllAsync();
                territoriesDto = TerritoryMapper.Mapper.Map<List<TerritoryResponse>>(territories);
                return new ResponseBuilder<IEnumerable<TerritoryResponse>> { Message = "OK.", Data = territoriesDto };
            }

            territories = await _territoryRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            territoriesDto = TerritoryMapper.Mapper.Map<List<TerritoryResponse>>(territories);
            return new PagedResponse<IEnumerable<TerritoryResponse>>(territoriesDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}