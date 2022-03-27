using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographic;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerDemographicHandlers
{
    public class DeleteCustomerDemographic : IRequestHandler<DeleteCustomerDemographicCommand, CustomerDemographicResponse>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;
        public DeleteCustomerDemographic(ICustomerDemographicRepository customerDemographicRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
        }
        public async Task<CustomerDemographicResponse> Handle(DeleteCustomerDemographicCommand request, CancellationToken cancellationToken)
        {
            var customerDemographicToDelete = _customerDemographicRepository.GetById(request.CustomerTypeId);
            if(customerDemographicToDelete == null)
            {
                return null;
            }

            _customerDemographicRepository.Delete(customerDemographicToDelete);
            return CustomerDemographicMapper.Mapper.Map<CustomerDemographicResponse>(customerDemographicToDelete);
        }
    }
}
