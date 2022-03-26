using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Shipper
{
    public record DeleteShipperCommand(int Id) : IRequest<ShipperResponse>;
}
