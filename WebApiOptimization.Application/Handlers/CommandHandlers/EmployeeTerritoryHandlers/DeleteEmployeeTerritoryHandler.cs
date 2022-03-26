using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeTerritory;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeTerritoryHandlers
{
    public class DeleteEmployeeTerritoryHandler : IRequestHandler<DeleteEmployeeTerritoryCommand, EmployeeTerritoryResponse>
    {
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
        public DeleteEmployeeTerritoryHandler(IEmployeeTerritoryRepository employeeTerritoryRepository)
        {
            _employeeTerritoryRepository = employeeTerritoryRepository;
        }
        public async Task<EmployeeTerritoryResponse> Handle(DeleteEmployeeTerritoryCommand request, CancellationToken cancellationToken)
        {
            var employeeTerritoryToDelete = _employeeTerritoryRepository.GetById(request.Id);
            if (employeeTerritoryToDelete == null)
            {
                return null;
            }

            _employeeTerritoryRepository.Delete(employeeTerritoryToDelete);
            var response = EmployeeMapper.Mapper.Map<EmployeeTerritoryResponse>(employeeTerritoryToDelete);
            return response;
        }
    }
}
