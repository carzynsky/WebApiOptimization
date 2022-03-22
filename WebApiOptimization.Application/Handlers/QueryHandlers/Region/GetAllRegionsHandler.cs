using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Queries.Region;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Region
{
    public class GetAllRegionsHandler : IRequestHandler<GetAllRegionsQuery, IEnumerable<RegionResponse>>
    {
        private readonly IRegionRepository _regionRepository; 
        public GetAllRegionsHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public Task<IEnumerable<RegionResponse>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
