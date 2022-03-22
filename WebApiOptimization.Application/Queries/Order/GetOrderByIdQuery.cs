using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Order
{
    public record GetOrderByIdQuery(int Id) : IRequest<OrderResponse>;
}
