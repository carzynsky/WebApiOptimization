using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderDetailQueries
{
    public record GetOrderDetailQuery(int? OrderId, int? ProductId) : IRequest<ResponseBuilder<IEnumerable<OrderDetailResponse>>>;
}
