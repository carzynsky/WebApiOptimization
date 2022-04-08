using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Employee;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeTerritoryRepository _employeeTerritoryRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, IEmployeeTerritoryRepository employeeTerritoryRepository, IOrderRepository orderRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeTerritoryRepository = employeeTerritoryRepository;
            _orderRepository = orderRepository;
        }

        public async Task<EmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToRemoveEntity = _employeeRepository.GetById(request.Id);
            if (employeeToRemoveEntity == null)
                return null;

            try
            {
                var employeeTerritoryEntityToRemove = _employeeTerritoryRepository.GetByEmployeeId(request.Id).ToList();
                if (employeeTerritoryEntityToRemove != null)
                    _employeeTerritoryRepository.DeleteRange(employeeTerritoryEntityToRemove);

                var orderEntityToRemove = _orderRepository.GetByEmployeeId(request.Id).ToList();
                if (orderEntityToRemove != null)
                    _orderRepository.DeleteRange(orderEntityToRemove);

                _employeeRepository.Delete(employeeToRemoveEntity);
                return EmployeeMapper.Mapper.Map<EmployeeResponse>(employeeToRemoveEntity);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
