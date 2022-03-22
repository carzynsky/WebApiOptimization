using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.Employee
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeResponse>;
}
