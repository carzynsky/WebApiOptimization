using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CategoryCommands
{
    public record DeleteCategoryCommand(int Id) : IRequest<ResponseBuilder<CategoryResponse>>;
}
