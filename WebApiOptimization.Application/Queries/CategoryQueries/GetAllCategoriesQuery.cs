using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CategoryQueries
{
    public record GetAllCategoriesQuery : IRequest<ResponseBuilder<IEnumerable<CategoryResponse>>>;
}
