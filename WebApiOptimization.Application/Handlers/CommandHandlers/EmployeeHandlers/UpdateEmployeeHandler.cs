using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Employee;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToUpdate = _employeeRepository.GetById(request.EmployeeId);
            if (employeeToUpdate == null)
            {
                return null;
            }

            var employeeToUpdateEntity = EmployeeMapper.Mapper.Map<Employee>(request);
            if(employeeToUpdateEntity == null)
            {
                return null;
            }

            _employeeRepository.Update(employeeToUpdateEntity);
            var response = EmployeeMapper.Mapper.Map<EmployeeResponse>(employeeToUpdateEntity);
            return response;
        }
    }
}
