using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.RegionCommands
{
    public record DeleteRegionCommand(int Id) : IRequest<ResponseBuilder<RegionResponse>>;
}
