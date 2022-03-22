using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Region
{
    public class CreateRegionCommand : IRequest<RegionResponse>
    {
        public string RegionDescription { get; set; }
    }
}
