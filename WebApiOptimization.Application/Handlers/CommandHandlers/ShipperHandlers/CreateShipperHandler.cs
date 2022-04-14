using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ShipperCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ShipperHandlers
{
    public class CreateShipperHandler : IRequestHandler<CreateShipperCommand, ResponseBuilder<ShipperResponse>>
    {
        private readonly IShipperRepository _shipperRepository;

        public CreateShipperHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<ResponseBuilder<ShipperResponse>> Handle(CreateShipperCommand request, CancellationToken cancellationToken)
        {
            var shipperEntity = ShipperMapper.Mapper.Map<Shipper>(request);
            if(shipperEntity == null)
            {
                return new ResponseBuilder<ShipperResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }
            try
            {
                var newShipper = await _shipperRepository.AddAsync(shipperEntity);
                var response = ShipperMapper.Mapper.Map<ShipperResponse>(newShipper);
                return new ResponseBuilder<ShipperResponse> { Message = "Shipper created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<ShipperResponse> { Message = $"Shipper not created! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
