using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Customer;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerHandlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public UpdateCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToUpdate = _customerRepository.GetById(request.CustomerId);
            if (customerToUpdate == null)
            {
                return null;
            }

            var customerToUpdateEntity = CustomerMapper.Mapper.Map<Customer>(request);
            if (customerToUpdateEntity == null)
            {
                return null;
            }

            _customerRepository.Update(customerToUpdateEntity);
            var response = CustomerMapper.Mapper.Map<CustomerResponse>(customerToUpdateEntity);
            return response;
        }
    }
}
