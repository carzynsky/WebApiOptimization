using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Product
{
    public record GetAllProductsQuery : IRequest<IReadOnlyList<ProductResponse>>;
}
