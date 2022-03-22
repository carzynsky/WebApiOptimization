using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Queries.EmployeeTerritory
{
    public record GetEmployeeTerritoryByIdQuery(int Id) : IRequest<EmployeeTerritoryResponse>;
}
