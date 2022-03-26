using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Region;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.RegionHandlers
{
    public class DeleteRegionHandler : IRequestHandler<DeleteRegionCommand, RegionResponse>
    {
        private readonly IRegionRepository _regionRepository;
        public DeleteRegionHandler(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<RegionResponse> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
        {
            var regionToDelete = _regionRepository.GetById(request.Id);
            if(regionToDelete == null)
            {
                return null;
            }

            _regionRepository.Delete(regionToDelete);
            var response = RegionMapper.Mapper.Map<RegionResponse>(regionToDelete);
            return response;
        }
    }
}
