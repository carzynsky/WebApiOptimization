using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Region
{
    public record DeleteRegionCommand : IRequest<RegionResponse>;
}
