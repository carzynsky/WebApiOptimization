using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.EmployeeTerritory;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.EmployeeTerritory
{
    public class GetEmployeeTerritoryByIdHandler : IRequestHandler<GetEmployeeTerritoryByIdQuery, EmployeeTerritoryResponse>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;

        public GetEmployeeTerritoryByIdHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }
        public async Task<EmployeeTerritoryResponse> Handle(GetEmployeeTerritoryByIdQuery request, CancellationToken cancellationToken)
        {
            var employeeTerritoryEntity = _employeeTerritoryRepository.GetById(request.Id);
            var response = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritoryResponse>(employeeTerritoryEntity);
            return response;
        }
    }
}
