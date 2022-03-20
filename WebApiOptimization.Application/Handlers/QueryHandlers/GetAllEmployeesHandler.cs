using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IReadOnlyList<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetAllEmployeesHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IReadOnlyList<EmployeeResponse>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = _employeeRepository.GetAll();
            var employeesResponse = EmployeeMapper.Mapper.Map<IReadOnlyList<EmployeeResponse>>(employees);
            return employeesResponse;
        }
    }
}
