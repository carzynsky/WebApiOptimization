using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries
{
    public class GetAllEmployeesQuery : IRequest<IReadOnlyList<EmployeeResponse>>
    {
    }
}
