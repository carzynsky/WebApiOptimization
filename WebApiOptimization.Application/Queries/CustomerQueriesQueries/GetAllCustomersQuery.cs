using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.CustomerQueries
{
    public class GetAllCustomersQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<CustomerResponse>>>
    {

    }
}
