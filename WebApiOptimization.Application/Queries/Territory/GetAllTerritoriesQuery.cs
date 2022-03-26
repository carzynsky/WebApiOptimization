using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Territory
{
    public record GetAllTerritoriesQuery : IRequest<IEnumerable<TerritoryResponse>>;
}
