using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.RegionQueries
{
    public record GetRegionByIdQuery(int Id) : IRequest<ResponseBuilder<RegionResponse>>;
}
