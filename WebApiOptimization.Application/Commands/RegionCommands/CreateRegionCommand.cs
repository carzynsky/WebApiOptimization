using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.RegionCommands
{
    public class CreateRegionCommand : IRequest<ResponseBuilder<RegionResponse>>
    {
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
    }
}
