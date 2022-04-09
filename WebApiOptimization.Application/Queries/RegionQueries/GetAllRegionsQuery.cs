using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.RegionQueries
{
    public record GetAllRegionsQuery : IRequest<ResponseBuilder<IEnumerable<RegionResponse>>>;
}
