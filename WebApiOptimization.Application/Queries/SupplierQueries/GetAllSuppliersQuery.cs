using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.SupplierQueries
{
    public record GetAllSuppliersQuery : IRequest<ResponseBuilder<IEnumerable<SupplierResponse>>>;
}
