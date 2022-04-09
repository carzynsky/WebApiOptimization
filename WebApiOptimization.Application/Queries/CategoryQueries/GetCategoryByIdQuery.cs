using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CategoryQueries
{
    public record GetCategoryByIdQuery(int Id) : IRequest<ResponseBuilder<CategoryResponse>>;

}
