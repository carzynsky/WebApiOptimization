using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.ShipperCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ShipperHandlers
{
    public class DeleteShipperHandler : IRequestHandler<DeleteShipperCommand, ResponseBuilder<ShipperResponse>>
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteShipperHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<ResponseBuilder<ShipperResponse>> Handle(DeleteShipperCommand request, CancellationToken cancellationToken)
        {
            var shipperToDeleteEntity = _shipperRepository.GetById(request.Id);
            if (shipperToDeleteEntity == null)
            {
                return new ResponseBuilder<ShipperResponse> { Message = $"Shipper with id={request.Id} not found!", Data = null };
            }

            // Find orders with this shipperId
            var ordersWithThisShipperId = _orderRepository.GetByShipperId(request.Id).ToList();
            if (ordersWithThisShipperId.Any())
            {
                ordersWithThisShipperId.ForEach(x => x.ShipVia = null);
                _orderRepository.UpdateRange(ordersWithThisShipperId);
            }

            _shipperRepository.Delete(shipperToDeleteEntity);
            var response = ShipperMapper.Mapper.Map<ShipperResponse>(shipperToDeleteEntity);
            return new ResponseBuilder<ShipperResponse> { Message = "Shipper deleted.", Data = response };
        }
    }
}
