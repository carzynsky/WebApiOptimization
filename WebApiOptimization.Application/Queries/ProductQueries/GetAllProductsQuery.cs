using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.ProductQueries
{
    public record GetAllProductsQuery : IRequest<ResponseBuilder<IEnumerable<ProductResponse>>>;
}
