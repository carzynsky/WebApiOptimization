using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeTerritory;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeTerritoryHandlers
{
    public class CreateEmployeeTerritoryHandler : IRequestHandler<CreateEmployeeTerritoryCommand, EmployeeTerritoryResponse>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
        public CreateEmployeeTerritoryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }
        public async Task<EmployeeTerritoryResponse> Handle(CreateEmployeeTerritoryCommand request, CancellationToken cancellationToken)
        {
            var employeeTerritoryEntity = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritory>(request);
            if (employeeTerritoryEntity == null)
            {
                return null;
            }

            var newEmployeeTerritory = _employeeTerritoryRepository.Add(employeeTerritoryEntity);
            if (newEmployeeTerritory == null)
            {
                return null;
            }

            var response = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritoryResponse>(newEmployeeTerritory);
            return response;
        }
    }
}
