using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.OrderDetailCommands
{
    public record DeleteOrderDetailCommand(int OrderID) : IRequest<ResponseBuilder<List<OrderDetailResponse>>>;
}
