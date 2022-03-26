using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Shipper;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ShipperHandlers
{
    public class CreateShipperHandler : IRequestHandler<CreateShipperCommand, ShipperResponse>
    {
        private readonly IShipperRepository _shipperRepository;
        public CreateShipperHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }
        public async Task<ShipperResponse> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
        {
            var shipperEntity = ShipperMapper.Mapper.Map<Shipper>(request);
            if(shipperEntity == null)
            {
                return null;
            }

            var newShipper = _shipperRepository.Add(shipperEntity);
            var response = ShipperMapper.Mapper.Map<ShipperResponse>(newShipper);
            return response;
        }
    }
}
