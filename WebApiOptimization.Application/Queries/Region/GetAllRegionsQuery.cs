using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Region
{
    public record GetAllRegionsQuery : IRequest<IEnumerable<RegionResponse>>;
}
