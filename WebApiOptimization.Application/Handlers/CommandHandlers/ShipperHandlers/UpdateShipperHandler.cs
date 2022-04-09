using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ShipperCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ShipperHandlers
{
    public class UpdateShipperHandler : IRequestHandler<UpdateShipperCommand, ResponseBuilder<ShipperResponse>>
    {
        private readonly IShipperRepository _shipperRepository;

        public UpdateShipperHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<ResponseBuilder<ShipperResponse>> Handle(UpdateShipperCommand request, CancellationToken cancellationToken)
        {
            var shipperToUpdate = _shipperRepository.GetById(request.ShipperId);
            if (shipperToUpdate == null)
            {
                return new ResponseBuilder<ShipperResponse> { Message = $"Shipper with id={request.ShipperId} not found!", Data = null };
            }

            var shipperToUpdateEntity = ShipperMapper.Mapper.Map<Shipper>(request);
            _shipperRepository.Update(shipperToUpdateEntity);
            var response = ShipperMapper.Mapper.Map<ShipperResponse>(shipperToUpdateEntity);
            return new ResponseBuilder<ShipperResponse> { Message = $"Shipper updated.", Data = response };
        }
    }
}
