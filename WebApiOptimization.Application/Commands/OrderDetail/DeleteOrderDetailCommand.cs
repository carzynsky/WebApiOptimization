using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.OrderDetail
{
    public record DeleteOrderDetailCommand(int OrderID) : IRequest<ResponseBuilder<List<OrderDetailResponse>>>;
}
