using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.EmployeeCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.EmployeeHandlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, ResponseBuilder<EmployeeResponse>>
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

        public async Task<ResponseBuilder<EmployeeResponse>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeToRemoveEntity = _employeeRepository.GetById(request.Id);
            if (employeeToRemoveEntity == null)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = $"Employee with id={request.Id} not found!", Data = null };
            }

            try
            {
                // Set reportsTo to null for employees
                var employeesWithThisReportsTo = _employeeRepository.GetByReportsTo(request.Id).ToList();
                if (employeesWithThisReportsTo.Any())
                {
                    employeesWithThisReportsTo.ForEach(x => x.ReportsTo = null);
                    _employeeRepository.UpdateRange(employeesWithThisReportsTo);
                }

                // Find employeeTerritories with this employeeId
                var employeeTerritoryEntityToRemove = _employeeTerritoryRepository.GetByEmployeeId(request.Id).ToList();
                if (employeeTerritoryEntityToRemove.Any())
                {
                    _employeeTerritoryRepository.DeleteRange(employeeTerritoryEntityToRemove);
                }


                // Set EmployeeId as null for each Order
                var ordersWithThisEmployeeId = _orderRepository.GetByEmployeeId(request.Id).ToList();
                if (ordersWithThisEmployeeId.Any())
                {
                    ordersWithThisEmployeeId.ForEach(x => x.EmployeeID = null);
                    _orderRepository.UpdateRange(ordersWithThisEmployeeId);
                }

                _employeeRepository.Delete(employeeToRemoveEntity);
                var response = EmployeeMapper.Mapper.Map<EmployeeResponse>(employeeToRemoveEntity);
                return new ResponseBuilder<EmployeeResponse> { Message = "Employee deleted.", Data = response };
            }
            catch (Exception e)
            {
                return new ResponseBuilder<EmployeeResponse> { Message = $"Employee not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
