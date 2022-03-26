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
    public class UpdateCustomerCustomerDemoHandler : IRequestHandler<UpdateCustomerCustomerDemoCommand, CustomerCustomerDemoResponse>
    {
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;
        public UpdateCustomerCustomerDemoHandler(ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }
        public async Task<CustomerCustomerDemoResponse> Handle(UpdateCustomerCustomerDemoCommand request, CancellationToken cancellationToken)
        {
            var customerCustomerDemoToUpdate = _customerCustomerDemoRepository.GetById(request.CustomerId);
            if(customerCustomerDemoToUpdate == null)
            {
                return null;
            }

            var customerCustomerDemoToUpdateEntity = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemo>(request);
            if(customerCustomerDemoToUpdateEntity == null)
            {
                return null;
            }

            _customerCustomerDemoRepository.Update(customerCustomerDemoToUpdateEntity);
            var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerCustomerDemoResponse>(customerCustomerDemoToUpdateEntity);
            return response;
        }
    }
}
