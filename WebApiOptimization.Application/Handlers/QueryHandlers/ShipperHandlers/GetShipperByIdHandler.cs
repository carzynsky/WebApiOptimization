using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.ShipperQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.ShipperHandlers
{
    public class GetShipperByIdHandler : IRequestHandler<GetShipperByIdQuery, ResponseBuilder<ShipperResponse>>
    {
        private readonly IShipperRepository _shipperRepository;

        public GetShipperByIdHandler(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<ResponseBuilder<ShipperResponse>> Handle(GetShipperByIdQuery request, CancellationToken cancellationToken)
        {
            var shipperEntity = await _shipperRepository.GetByIdAsync(request.Id);
            if(shipperEntity == null)
            {
                return new ResponseBuilder<ShipperResponse> { Message = $"Shipper with id={request.Id} not found!", Data = null };
            }

            var response = ShipperMapper.Mapper.Map<ShipperResponse>(shipperEntity);
            return new ResponseBuilder<ShipperResponse> { Message = "OK.", Data = response };
        }
    }
}
