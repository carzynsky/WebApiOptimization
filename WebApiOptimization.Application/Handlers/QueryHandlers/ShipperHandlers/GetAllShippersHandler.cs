using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.ShipperQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Shipper
{
    public class GetAllShippersHandler : IRequestHandler<GetAllShippersQuery, ResponseBuilder<IEnumerable<ShipperResponse>>>
    {
        private readonly IShipperRepository _shipperRepository;

        public GetAllShippersHandler(IShipperRepository shipperRepository)
        { 
            _shipperRepository = shipperRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<ShipperResponse>>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
        {
            var shippers = await _shipperRepository.GetAllAsync();
            var response = ShipperMapper.Mapper.Map<IEnumerable<ShipperResponse>>(shippers);
            return new ResponseBuilder<IEnumerable<ShipperResponse>> { Message = "OK.", Data = response };
        }
    }
}
