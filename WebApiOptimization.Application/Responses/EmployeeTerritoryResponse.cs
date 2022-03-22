using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class EmployeeTerritoryResponse
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string TerritoryId { get; set; }
        public Territory Territory { get; set; }
    }
}
