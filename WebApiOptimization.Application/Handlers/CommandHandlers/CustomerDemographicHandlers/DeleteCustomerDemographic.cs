using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiOptimization.Application.Commands.CustomerDemographicCommands;
using WebApiOptimization.Application.Mappers;
using WebApiOptimization.Application.Responses;
using WebApiOptimization.Core.Repositories;

namespace WebApiOptimization.Application.Handlers.CommandHandlers.CustomerDemographicHandlers
{
    public class DeleteCustomerDemographic : IRequestHandler<DeleteCustomerDemographicCommand, ResponseBuilder<CustomerDemographicResponse>>
    {
        private readonly ICustomerDemographicRepository _customerDemographicRepository;
        private readonly ICustomerCustomerDemoRepository _customerCustomerDemoRepository;

        public DeleteCustomerDemographic(ICustomerDemographicRepository customerDemographicRepository, ICustomerCustomerDemoRepository customerCustomerDemoRepository)
        {
            _customerDemographicRepository = customerDemographicRepository;
            _customerCustomerDemoRepository = customerCustomerDemoRepository;
        }

        public async Task<ResponseBuilder<CustomerDemographicResponse>> Handle(DeleteCustomerDemographicCommand request, CancellationToken cancellationToken)
        {
            var customerDemographicToDelete = _customerDemographicRepository.GetByCustomerTypeId(request.CustomerTypeId);
            if(customerDemographicToDelete == null)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = $"CustomerDemographic with id={request.CustomerTypeId} not found!", Data = null };
            }

            try
            {
                // Find CustomerCustomerDemos with this CustomerTypeId
                var customerCustomerDemosWithThisCustomerTypeId = _customerCustomerDemoRepository.GetByCustomerTypeId(request.CustomerTypeId).ToList();
                if (customerCustomerDemosWithThisCustomerTypeId.Any())
                {
                    // Remove entries
                    _customerCustomerDemoRepository.DeleteRange(customerCustomerDemosWithThisCustomerTypeId);
                }

                _customerDemographicRepository.Delete(customerDemographicToDelete);
                var response = CustomerDemographicMapper.Mapper.Map<CustomerDemographicResponse>(customerDemographicToDelete);
                return new ResponseBuilder<CustomerDemographicResponse> { Message = "CustomerDemographic deleted.", Data = response };
            }
            catch(Exception e)
            {
                return new ResponseBuilder<CustomerDemographicResponse> { Message = $"CustomerDemographic not deleted! Error: {e.InnerException.Message}", Data = null };
            }
        }
    }
}
