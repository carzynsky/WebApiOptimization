using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Employee
{
    public record GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeResponse>>;
 
}
