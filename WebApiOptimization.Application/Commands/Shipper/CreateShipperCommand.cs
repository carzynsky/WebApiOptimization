using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Shipper
{
    public class CreateShipperCommand : IRequest<ShipperResponse>
    {
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
