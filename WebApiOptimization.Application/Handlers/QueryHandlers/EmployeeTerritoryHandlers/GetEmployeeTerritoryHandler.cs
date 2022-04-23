using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.EmployeeTerritoryQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.EmployeeTerritoryHandlers
{
    public class GetEmployeeTerritoryHandler : IRequestHandler<GetEmployeeTerritoriesQuery, ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>>>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public GetEmployeeTerritoryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>>> Handle(GetEmployeeTerritoriesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<EmployeeTerritory> employeeTerritories;

            if (request.TerritoryId == null && request.EmployeeId == null)
            {
                employeeTerritories = await _employeeTerritoryRepository.GetAllAsync();
            }
            else if(request.EmployeeId != null && request.TerritoryId == null)
            {
                employeeTerritories = await _employeeTerritoryRepository.GetByEmployeeIdAsync((int)request.EmployeeId, true);
            }
            else if(request.EmployeeId == null && request.TerritoryId != null)
            {
                employeeTerritories = await _employeeTerritoryRepository.GetByTerritoryIdAsync(request.TerritoryId, true);
            }
            else
            {
                employeeTerritories = await _employeeTerritoryRepository.GetByEmployeeIdAndTerritoryIdAsync((int)request.EmployeeId, request.TerritoryId, true);
            }

            var response = EmployeeTerritoryMapper.Mapper.Map<IEnumerable<EmployeeTerritoryResponse>>(employeeTerritories);
            return new ResponseBuilder<IEnumerable<EmployeeTerritoryResponse>> { Message = "OK.", Data = response };
        }
    }
}
