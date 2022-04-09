using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.EmployeeQueries
{
    public record GetEmployeeByIdQuery(int Id) : IRequest<ResponseBuilder<EmployeeResponse>>;
}
