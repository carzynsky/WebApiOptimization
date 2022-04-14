using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Queries.EmployeeQueries;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.QueryHandlers.Employee
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, ResponseBuilder<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseBuilder<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employeeEntity = await _employeeRepository.GetByIdAsync(request.Id, true);
            if (employeeEntity == null)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = $"Employee with id={request.Id} not found!", Data = null };
            }

            var employeeResponse = EmployeeMapper.Mapper.Map<EmployeeResponse>(employeeEntity);
            return new ResponseBuilder<EmployeeResponse> { Message = "OK", Data = employeeResponse };
        }
    }
}
