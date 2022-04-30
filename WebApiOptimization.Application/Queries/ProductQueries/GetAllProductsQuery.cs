using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.ProductQueries
{
    public class GetAllProductsQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<ProductResponse>>>
    {

    }
}
