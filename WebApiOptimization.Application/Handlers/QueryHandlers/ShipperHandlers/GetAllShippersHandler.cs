using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.ShipperQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.ShipperHandlers
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
            List<Shipper> shippers;
            List<ShipperResponse> shippersDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                shippers = await _shipperRepository.GetAllAsync();
                shippersDto = ShipperMapper.Mapper.Map<List<ShipperResponse>>(shippers);
                return new ResponseBuilder<IEnumerable<ShipperResponse>> { Message = "OK.", Data = shippersDto };
            }

            shippers = await _shipperRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            shippersDto = ShipperMapper.Mapper.Map<List<ShipperResponse>>(shippers);
            return new PagedResponse<IEnumerable<ShipperResponse>>(shippersDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
