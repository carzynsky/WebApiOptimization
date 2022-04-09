using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.EmployeeTerritoryCommands
{
    public record DeleteEmployeeTerritoryCommand(int? EmployeeId, string TerritoryId) : IRequest<ResponseBuilder<List<EmployeeTerritoryResponse>>>;
}
