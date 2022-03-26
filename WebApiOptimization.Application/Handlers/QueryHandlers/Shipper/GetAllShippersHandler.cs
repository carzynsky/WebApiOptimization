using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.Shipper;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Shipper
{
    public class GetAllShippersHandler : IRequestHandler<GetAllShippersQuery, IEnumerable<ShipperResponse>>
    {
        private readonly IShipperRepository _shipperRepository;
        public GetAllShippersHandler(IShipperRepository shipperRepository)
        { 
            _shipperRepository = shipperRepository;
        }
        public async Task<IEnumerable<ShipperResponse>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
        {
            var shippers = _shipperRepository.GetAll();
            var response = ShipperMapper.Mapper.Map<IEnumerable<ShipperResponse>>(shippers);
            return response;
        }
    }
}
