using MediatR;
using System.Collections.Generic;
using WebApiOptimization.Application.Filter;
using WebApiOptimization.Application.Responses;
namespace WebApiOptimization.Application.Queries.EmployeeTerritoryQueries
{
    public class GetEmployeeTerritoriesQuery : PaginationFilter, IRequest<ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>>>
    {
        public int? EmployeeId { get; set; }
        public string TerritoryId { get; set; }
    }
}
