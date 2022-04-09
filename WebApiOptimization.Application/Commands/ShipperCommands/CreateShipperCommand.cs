using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.ShipperCommands
{
    public class CreateShipperCommand : IRequest<ResponseBuilder<ShipperResponse>>
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
