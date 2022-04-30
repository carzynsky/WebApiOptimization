using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;
namespace WebApiOptimization.Application.Queries.RegionQueries
{
    public class GetAllRegionsQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<RegionResponse>>>
    {

    }
}
