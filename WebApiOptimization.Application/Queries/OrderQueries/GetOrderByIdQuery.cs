using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderQueries
{
    public record GetOrderByIdQuery(int Id) : IRequest<ResponseBuilder<OrderResponse>>;
}
