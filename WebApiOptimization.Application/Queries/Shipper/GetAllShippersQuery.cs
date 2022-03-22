using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Shipper
{
    public record GetAllShippersQuery : IRequest<IReadOnlyList<ShipperResponse>>;
}
