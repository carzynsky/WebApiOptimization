using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.EmployeeQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Employee
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
            var employees = _employeeRepository.GetAll();
            var employeesResponse = EmployeeMapper.Mapper.Map<IEnumerable<EmployeeResponse>>(employees);
            return new ResponseBuilder<IEnumerable<EmployeeResponse>> { Message = "OK", Data = employeesResponse };
        }
    }
}
