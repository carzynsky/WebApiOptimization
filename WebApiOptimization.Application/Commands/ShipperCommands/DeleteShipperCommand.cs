using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.ShipperCommands
{
    public record DeleteShipperCommand(int Id) : IRequest<ResponseBuilder<ShipperResponse>>;
}
