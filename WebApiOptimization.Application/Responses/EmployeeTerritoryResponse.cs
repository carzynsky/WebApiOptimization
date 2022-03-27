using WebApiOptimization.Core.Entities;

namespace WebApiOptimization.Application.Responses
{
    public class EmployeeTerritoryResponse
    {
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public string TerritoryID { get; set; }
        public Territory Territory { get; set; }
    }
}
