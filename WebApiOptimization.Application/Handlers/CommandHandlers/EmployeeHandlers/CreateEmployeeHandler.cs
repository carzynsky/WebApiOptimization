using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, ResponseBuilder<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseBuilder<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeEntity = EmployeeMapper.Mapper.Map<Employee>(request);
            if(employeeEntity == null)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }
            try
            {
                var newEmployee = await _employeeRepository.AddAsync(employeeEntity);
                var employeeResponse = EmployeeMapper.Mapper.Map<EmployeeResponse>(newEmployee);
                return new ResponseBuilder<EmployeeResponse> { Message = "Employee created.", Data = employeeResponse };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = $"Employee not created! Error: {e.InnerException.Message}", Data = null };
            }
            
        }
    }
}
