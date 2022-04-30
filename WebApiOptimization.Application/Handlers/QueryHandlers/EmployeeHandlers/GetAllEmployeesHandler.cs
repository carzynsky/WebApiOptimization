using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.EmployeeQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Application.Wrappers;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.EmployeeHandlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, ResponseBuilder<IEnumerable<EmployeeResponse>>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseBuilder<IEnumerable<EmployeeResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            List<Employee> employees;
            List<EmployeeResponse> employeesDto;

            if(request.PageNumber == 0 || request.PageSize == 0)
            {
                employees = await _employeeRepository.GetAllAsync();
                employeesDto = EmployeeMapper.Mapper.Map<List<EmployeeResponse>>(employees);
                return new ResponseBuilder<IEnumerable<EmployeeResponse>> { Message = "OK", Data = employeesDto };
            }

            employees = await _employeeRepository.GetAllPagedAsync(request.PageNumber, request.PageSize);
            employeesDto = EmployeeMapper.Mapper.Map<List<EmployeeResponse>>(employees);
            return new PagedResponse<IEnumerable<EmployeeResponse>>(employeesDto, request.PageNumber, request.PageSize, "OK");
        }
    }
}
