using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Region
{
    public class UpdateRegionCommand : IRequest<RegionResponse>
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
    }
}
