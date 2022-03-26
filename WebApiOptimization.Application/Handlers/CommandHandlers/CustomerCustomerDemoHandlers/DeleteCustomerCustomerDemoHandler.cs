using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCustomerDemo;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerCustomerDemoHandlers
{
    public class DeleteCustomerCustomerDemoHandler : IRequestHandler<DeleteCustomerCustomerDemoCommand, CustomerCustomerDemoResponse>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public DeleteCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }

        public async Task<CustomerCustomerDemoResponse> Handle(DeleteCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
        {
            var customerCustomerDemoToDelete = _customerCustomerDemoRepository.GetById(request.Id);
            if(customerCustomerDemoToDelete == null)
            {
                return null;
            }

            _customerCustomerDemoRepository.Delete(customerCustomerDemoToDelete);
            var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemoResponse>(customerCustomerDemoToDelete);
            return response;
        }
    }
}
