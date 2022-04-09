using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.RegionCommands
{
    public class UpdateRegionCommand : IRequest<ResponseBuilder<RegionResponse>>
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
    }
}
