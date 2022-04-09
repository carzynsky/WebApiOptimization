using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.ProductQueries
{
    public record GetProductByIdQuery(int Id) : IRequest<ResponseBuilder<ProductResponse>>;
}
