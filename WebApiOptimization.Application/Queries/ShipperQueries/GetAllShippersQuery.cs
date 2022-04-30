using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;
namespace WebApiOptimization.Application.Queries.ShipperQueries
{
    public class GetAllShippersQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<ShipperResponse>>>
    {
    }
}
