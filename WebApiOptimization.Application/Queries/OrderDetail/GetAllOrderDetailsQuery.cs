using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.OrderDetail
{
    public record GetAllOrderDetailsQuery : IRequest<IReadOnlyList<OrderDetailResponse>>;
}
