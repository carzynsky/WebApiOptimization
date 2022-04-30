using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;
namespace WebApiOptimization.Application.Queries.SupplierQueries
{
    public class GetAllSuppliersQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<SupplierResponse>>>
    {

    }
}
