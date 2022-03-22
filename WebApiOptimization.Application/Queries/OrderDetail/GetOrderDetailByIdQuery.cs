using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderDetail
{
    public record GetOrderDetailByIdQuery(int Id) : IRequest<OrderDetailResponse>;
}
