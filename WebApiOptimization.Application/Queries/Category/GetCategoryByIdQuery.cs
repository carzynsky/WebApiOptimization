using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Category
{
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryResponse>;

}
