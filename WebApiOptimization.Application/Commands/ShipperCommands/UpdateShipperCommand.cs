using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.ShipperCommands
{
    public class UpdateShipperCommand : IRequest<ResponseBuilder<ShipperResponse>>
    {
        public int ShipperId { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
