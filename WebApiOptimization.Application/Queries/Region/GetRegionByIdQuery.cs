using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Region
{
    public record GetRegionByIdQuery(int Id) : IRequest<RegionResponse>;
}
