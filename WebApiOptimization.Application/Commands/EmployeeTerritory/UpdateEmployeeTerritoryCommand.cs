using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.EmployeeTerritory
{
    public class UpdateEmployeeTerritoryCommand : IRequest<EmployeeTerritoryResponse>
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }
    }
}
