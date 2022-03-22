using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.OrderDetail
{
    public record DeleteOrderDetailCommand(int Id) : IRequest<OrderDetailResponse>;
}
