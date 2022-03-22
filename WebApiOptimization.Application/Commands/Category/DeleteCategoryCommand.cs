using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Category
{
    public record DeleteCategoryCommand(int Id) : IRequest<CategoryResponse>;
}
