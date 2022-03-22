using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Employee;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRespository;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRespository = employeeRepository;
        }

        public async Task<EmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToUpdateEntity = _employeeRespository.GetById(request.EmployeeId);
            if (employeeToUpdateEntity == null)
                return null;

            _employeeRespository.Update(employeeToUpdateEntity);
            return Mappers.EmployeeMapper.Mapper.Map<EmployeeResponse>(employeeToUpdateEntity);
        }
    }
}
