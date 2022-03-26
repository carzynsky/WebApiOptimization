using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Shipper;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Shipper
{
    public class GetShipperByIdHandler : IRequestHandler<GetShipperByIdQuery, ShipperResponse>
    {
        private readonly IShipperRepository _shipperRepository;
        public GetShipperByIdHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }
        public async Task<ShipperResponse> Handle(GetShipperByIdQuery request, CancellationToken cancellationToken)
        {
            var shipperEntity = _shipperRepository.GetById(request.Id);
            var response = ShipperMapper.Mapper.Map<ShipperResponse>(shipperEntity);
            return response;
        }
    }
}
