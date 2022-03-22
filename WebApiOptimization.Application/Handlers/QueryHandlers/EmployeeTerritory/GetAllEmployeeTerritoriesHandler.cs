using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Queries.EmployeeTerritory;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.EmployeeTerritory
{
    public class GetAllEmployeeTerritoriesHandler : IRequestHandler<GetAllEmployeeTerritoriesQuery, IEnumerable<EmployeeTerritoryResponse>>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public GetAllEmployeeTerritoriesHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }

        public Task<IEnumerable<EmployeeTerritoryResponse>> Handle(GetAllEmployeeTerritoriesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
