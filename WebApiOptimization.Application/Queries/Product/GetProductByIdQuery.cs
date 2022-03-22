using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Product
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductResponse>;
}
