using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Order
{
    public record GetAllOrdersQuery : IRequest<List<OrderResponse>>;
}
