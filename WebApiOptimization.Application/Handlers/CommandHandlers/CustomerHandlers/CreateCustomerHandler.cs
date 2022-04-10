using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCommands;
using WebApiOptimization.Application.Helpers;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerHandlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, ResponseBuilder<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ResponseBuilder<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerEntity = CustomerMapper.Mapper.Map<Customer>(request);
            if(customerEntity == null)
            {
                return new ResponseBuilder<CustomerResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            try
            {
                var newCustomer = _customerRepository.Add(customerEntity);
                var response = CustomerMapper.Mapper.Map<CustomerResponse>(newCustomer);
                return new ResponseBuilder<CustomerResponse> { Message = "Customer created.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CustomerResponse> { Message = $"Customer not created! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
