using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.EmployeeTerritory
{
    public record DeleteEmployeeTerritoryCommand : IRequest<EmployeeTerritoryResponse>;
}
