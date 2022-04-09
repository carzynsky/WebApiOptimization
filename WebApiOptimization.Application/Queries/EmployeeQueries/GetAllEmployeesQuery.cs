using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.EmployeeQueries
{
    public record GetAllEmployeesQuery : IRequest<ResponseBuilder<IEnumerable<EmployeeResponse>>>;
 
}
