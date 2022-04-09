using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.RegionCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.RegionHandlers
{
    public class CreateRegionHandler : IRequestHandler<CreateRegionCommand, ResponseBuilder<RegionResponse>>
    {
        private readonly IRegionRepository _regionRepository;

        public CreateRegionHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        public async Task<ResponseBuilder<RegionResponse>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            var regionEntity = RegionMapper.Mapper.Map<Region>(request);
            if(regionEntity == null)
            {
                return new ResponseBuilder<RegionResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newRegion = _regionRepository.Add(regionEntity);
                var response = RegionMapper.Mapper.Map<RegionResponse>(newRegion);
                return new ResponseBuilder<RegionResponse> { Message = "Region created", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<RegionResponse> { Message = $"Region not created! Error: {e.Message}", Data = null };
            }
            
        }
    }
}
