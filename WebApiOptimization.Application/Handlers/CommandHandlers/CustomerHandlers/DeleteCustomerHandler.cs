using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.Customer;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerHandlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CustomerResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerToDelete = _customerRepository.GetById(request.Id);
            if(customerToDelete == null)
            {
                return null;
            }

            _customerRepository.Delete(customerToDelete);
            var response = CustomerMapper.Mapper.Map<CustomerResponse>(customerToDelete);
            return response;
        }
    }
}
