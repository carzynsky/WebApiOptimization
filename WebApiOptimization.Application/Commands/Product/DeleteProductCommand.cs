using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.Product
{
    public record DeleteProductCommand(int Id) : IRequest<ProductResponse>;
}
