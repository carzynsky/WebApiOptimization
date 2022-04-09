using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.EmployeeCommands
{
    public record DeleteEmployeeCommand(int Id) : IRequest<ResponseBuilder<EmployeeResponse>>;
}
