using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Category
{
    public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResponse>>;
}
