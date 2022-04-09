using MediatR;
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
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, ResponseBuilder<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ResponseBuilder<CustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToUpdate = _customerRepository.GetById(request.CustomerId);
            if (customerToUpdate == null)
            {
                return new ResponseBuilder<CustomerResponse> { Message = $"Customer with id={request.CustomerId} not found!", Data = null };
            }

            var customerToUpdateEntity = CustomerMapper.Mapper.Map<Customer>(request);
            if (customerToUpdateEntity == null)
            {
                return new ResponseBuilder<CustomerResponse> { Message = ResponseBuilderHelper.InvalidData, Data = null };
            }

            _customerRepository.Update(customerToUpdateEntity);
            var response = CustomerMapper.Mapper.Map<CustomerResponse>(customerToUpdateEntity);
            return new ResponseBuilder<CustomerResponse> { Message = "Customer updated", Data = null };
        }
    }
}
