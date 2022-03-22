using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.EmployeeTerritory
{
    public class CreateEmployeeTerritoryCommand : IRequest<EmployeeTerritoryResponse>
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }
    }
}
