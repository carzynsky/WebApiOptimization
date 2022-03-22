using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public Task<IEnumerable<ShipperResponse>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
