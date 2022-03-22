using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Shipper
{
    public record GetShipperByIdQuery(int Id) : IRequest<ShipperResponse>;
}
