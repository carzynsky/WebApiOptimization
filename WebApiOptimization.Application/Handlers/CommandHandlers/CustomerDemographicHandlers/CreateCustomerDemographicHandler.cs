using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographic;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Entities;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerDemographicHandlers
{
    public class CreateCustomerDemographicHandler : IRequestHandler<CreateCustomerDemographicCommand, CustomerDemographicResponse>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;
        public CreateCustomerDemographicHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }
        public async Task<CustomerDemographicResponse> Handle(CreateCustomerDemographicCommand request, CancellationToken cancellationToken)
        {
            var customerDemographicEntity = CustomerDemographicMapper.Mapper.Map<CustomerDemographic>(request);
            if(customerDemographicEntity == null)
            {
                return null;
            }
            var newCustomerDemographic = _customerDemographicRepository.Add(customerDemographicEntity);
            var response = CustomerCustomerDemoMapper.Mapper.Map<CustomerDemographicResponse>(newCustomerDemographic);
            return response;
        }
    }
}
