using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, ResponseBuilder<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseBuilder<EmployeeResponse>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToUpdate = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employeeToUpdate == null)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = $"Employee with id={request.EmployeeId} not found!", Data = null };
            }

            try
            {
                var employeeToUpdateEntity = EmployeeMapper.Mapper.Map<Employee>(request);
                _employeeRepository.Update(employeeToUpdateEntity);
                var response = EmployeeMapper.Mapper.Map<EmployeeResponse>(employeeToUpdateEntity);
                return new ResponseBuilder<EmployeeResponse> { Message = "Employee updated.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = $"Employee not updated! Error: {e.InnerException.Message}", Data = null };
            }
            
        }
    }
}
