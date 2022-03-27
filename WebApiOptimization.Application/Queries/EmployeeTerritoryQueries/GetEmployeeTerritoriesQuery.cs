using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.EmployeeTerritoryQueries
{
    public record GetEmployeeTerritoriesQuery(int? EmployeeId, string TerritoryId) : IRequest<IEnumerable<EmployeeTerritoryResponse>>;
}
