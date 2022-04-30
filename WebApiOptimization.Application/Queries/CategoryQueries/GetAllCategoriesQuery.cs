using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CategoryQueries
{
    public class GetAllCategoriesQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<CategoryResponse>>>
    {

    }
}
