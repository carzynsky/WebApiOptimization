using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;
namespace WebApiOptimization.Application.Queries.TerritoryQueries
{
    public class GetAllTerritoriesQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<TerritoryResponse>>>
    {

    }
}
