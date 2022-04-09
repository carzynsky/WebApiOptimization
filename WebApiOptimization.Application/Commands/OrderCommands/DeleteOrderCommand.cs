using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.OrderCommands
{
    public record DeleteOrderCommand(int Id) : IRequest<ResponseBuilder<OrderResponse>>;
}
