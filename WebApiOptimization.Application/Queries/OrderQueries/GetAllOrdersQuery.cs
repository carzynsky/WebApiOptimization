using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderQueries
{
    public record GetAllOrdersQuery : IRequest<ResponseBuilder<IEnumerable<OrderResponse>>>;
}
