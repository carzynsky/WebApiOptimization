using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerHandlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, ResponseBuilder<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public DeleteCustomerHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ResponseBuilder<CustomerResponse>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToDelete = _customerRepository.GetById(request.CustomerId);
            if(customerToDelete == null)
            {
                return new ResponseBuilder<CustomerResponse> { Message = "Customer not found!", Data = null };
            }

            try
            {
                // Find orders with this customer
                var ordersWithThisCustomer = _orderRepository.GetByCustomerId(request.CustomerId).ToList();
                if (ordersWithThisCustomer.Any())
                {
                    // Set CustomerId as null for each order
                    ordersWithThisCustomer.ForEach(x => x.CustomerID = null);
                    _orderRepository.UpdateRange(ordersWithThisCustomer);
                }

                _customerRepository.Delete(customerToDelete);
                var response = CustomerMapper.Mapper.Map<CustomerResponse>(customerToDelete);
                return new ResponseBuilder<CustomerResponse> { Message = "Customer deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CustomerResponse> { Message = $"Customer not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
