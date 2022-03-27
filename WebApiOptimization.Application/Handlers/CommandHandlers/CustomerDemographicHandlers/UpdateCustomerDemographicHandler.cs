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
    public class UpdateCustomerDemographicHandler : IRequestHandler<UpdateCustomerDemographicCommand, CustomerDemographicResponse>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;
        public UpdateCustomerDemographicHandler(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }
        public async Task<CustomerDemographicResponse> Handle(UpdateCustomerDemographicCommand request, CancellationToken cancellationToken)
        {
            var customerDemographicToUpdate = _customerDemographicRepository.GetById(int.Parse(request.CustomerTypeId));
            if(customerDemographicToUpdate == null)
            {
                return null;
            }

            var customerDemographicToUpdateEntity = CustomerDemographicMapper.Mapper.Map<CustomerDemographic>(request);
            _customerDemographicRepository.Update(customerDemographicToUpdateEntity);
            return CustomerCustomerDemoMapper.Mapper.Map<CustomerDemographicResponse>(customerDemographicToUpdateEntity);
        }
    }
}
