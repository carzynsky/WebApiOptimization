using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerCustomerDemo;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerCustomerDemoHandlers
{
    public class CreateCustomerCustomerDemoHandler : IRequestHandler<CreateCustomerCustomerDemoCommand, CustomerCustomerDemoResponse>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public CreateCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }

        public async Task<CustomerCustomerDemoResponse> Handle(CreateCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
        {
            var customerCustomerDemoEntity = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemo>(request);
            if(customerCustomerDemoEntity == null)
            {
                return null;
            }

            var newCustomerCustomerDemo = _customerCustomerDemoRepository.Add(customerCustomerDemoEntity);
            if(newCustomerCustomerDemo == null)
            {
                return null;
            }

            var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemoResponse>(newCustomerCustomerDemo);
            return response;
        }
    }
}
