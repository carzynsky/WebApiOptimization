using MediatR;
using WebApiOptimization.Application.Responses;

namespace WebApiOptimization.Application.Commands.EmployeeTerritoryCommands
{
    public class CreateEmployeeTerritoryCommand : IRequest<ResponseBuilder<EmployeeTerritoryResponse>>
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }
    }
}
