using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Order
{
    public record DeleteOrderCommand(int Id) : IRequest<OrderResponse>;
}
