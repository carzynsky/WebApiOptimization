using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.TerritoryQueries
{
    public record GetAllTerritoriesQuery : IRequest<ResponseBuilder<IEnumerable<TerritoryResponse>>>;
}
