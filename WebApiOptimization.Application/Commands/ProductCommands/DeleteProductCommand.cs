using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.ProductCommands
{
    public record DeleteProductCommand(int Id) : IRequest<ResponseBuilder<ProductResponse>>;
}
