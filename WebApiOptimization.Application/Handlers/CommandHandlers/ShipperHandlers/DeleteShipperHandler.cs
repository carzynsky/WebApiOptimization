using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Shipper;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.ShipperHandlers
{
    public class DeleteShipperHandler : IRequestHandler<DeleteShipperCommand, ShipperResponse>
    {
        private readonly IShipperRepository _shipperRepository;
        public DeleteShipperHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }
        public async Task<ShipperResponse> Handle(DeleteShipperCommand request, CancellationToken cancellationToken)
        {
            var shipperToDeleteEntity = _shipperRepository.GetById(request.Id);
            if (shipperToDeleteEntity == null)
            {
                return null;
            }

            _shipperRepository.Delete(shipperToDeleteEntity);
            var response = ShipperMapper.Mapper.Map<ShipperResponse>(shipperToDeleteEntity);
            return response;
        }
    }
}
