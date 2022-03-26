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
    public class UpdateEmployeeTerritoryHandler : IRequestHandler<UpdateEmployeeTerritoryCommand, EmployeeTerritoryResponse>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
        public UpdateEmployeeTerritoryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }
        public async Task<EmployeeTerritoryResponse> Handle(UpdateEmployeeTerritoryCommand request, CancellationToken cancellationToken)
        {
            var employeeTerritoryToUpdate = _employeeTerritoryRepository.GetById(request.EmployeeId);
            if (employeeTerritoryToUpdate == null)
            {
                return null;
            }

            var employeeTerritoryToUpdateEntity = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritory>(request);
            if (employeeTerritoryToUpdateEntity == null)
            {
                return null;
            }

            _employeeTerritoryRepository.Update(employeeTerritoryToUpdateEntity);
            var response = EmployeeTerritoryMapper.Mapper.Map<EmployeeTerritoryResponse>(employeeTerritoryToUpdateEntity);
            return response;
        }
    }
}
