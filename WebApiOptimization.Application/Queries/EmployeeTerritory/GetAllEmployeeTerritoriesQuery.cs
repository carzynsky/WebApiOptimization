using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.EmployeeTerritory
{
    public record GetAllEmployeeTerritoriesQuery : IRequest<IReadOnlyList<EmployeeTerritoryResponse>>;
}
