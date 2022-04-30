using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.EmployeeQueries
{
    public class GetAllEmployeesQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<EmployeeResponse>>>
    {

    }
}
