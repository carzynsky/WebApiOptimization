using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Shipper
{
    public record DeleteShipperCommand : IRequest<ShipperResponse>;
}
