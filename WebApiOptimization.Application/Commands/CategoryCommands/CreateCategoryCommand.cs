using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.CategoryCommands
{
    public class CreateCategoryCommand : IRequest<ResponseBuilder<CategoryResponse>>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
