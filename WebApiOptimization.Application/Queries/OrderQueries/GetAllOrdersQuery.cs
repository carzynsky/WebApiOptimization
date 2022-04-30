using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderQueries
{
    public class GetAllOrdersQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<OrderResponse>>>
    {

    }
}
