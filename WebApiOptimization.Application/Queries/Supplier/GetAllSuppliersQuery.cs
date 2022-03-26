using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Supplier
{
    public record GetAllSuppliersQuery : IRequest<IEnumerable<SupplierResponse>>;
}
